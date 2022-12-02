using Microsoft.EntityFrameworkCore;
using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;
using SamplePoc.Sql.Extensions;

namespace SamplePoc.Sql.Repositories
{
    public class KeywordRepository : IKeywordRepository
    {
        private readonly MarketingSuiteContext _dbContext;

        public KeywordRepository(MarketingSuiteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Keyword keyword)
        {
            var primarySourceIds = keyword.PrimarySources.Select(x => x.Id).ToHashSet();

            var keywordPrimary = await _dbContext.KeywordsPrimaries
                .Include(keywordPrimary => keywordPrimary.KeywordsSourcePrimaries)
                .Where(keywordPrimary => keywordPrimary.Name.Equals(keyword.Name))
                .SingleOrDefaultAsync();

            var primarySources = await _dbContext.SourcePrimaries.Where(x => primarySourceIds.Contains(x.Id)).ToListAsync();

            if (keywordPrimary != null)
            {
                var existingKeywordSourceIds = keywordPrimary.KeywordsSourcePrimaries.Select(x => x.PrimarySourceId).ToHashSet();
                
                primarySources
                    .Where(x => !existingKeywordSourceIds.Contains(x.Id))
                    .ToList()
                    .ForEach(source => source.KeywordsSourcePrimaries.Add(new Entities.KeywordsSourcePrimary
                    {
                        PrimarySource = source,
                        KeywordsPrimary = keywordPrimary
                    }));

                return true;
            }
            else
            {
                var keywordPrimaryEntity = await _dbContext.AddAsync(keyword.ToEntity());

                primarySources
                    .ForEach(source => source.KeywordsSourcePrimaries.Add(new Entities.KeywordsSourcePrimary
                    {
                        PrimarySource = source,
                        KeywordsPrimary = keywordPrimaryEntity.Entity
                    }));

                return false;
            }
        }

        public async Task<IEnumerable<string>> BulkAddAsync(IEnumerable<Keyword> keywords)
        {
            var validations = new List<string>();

            foreach (var keyword in keywords)
            {
                var primarySourceIds = keyword.PrimarySources.Select(x => x.Id).ToHashSet();

                var keywordPrimary = await _dbContext.KeywordsPrimaries
                    .Include(keywordPrimary => keywordPrimary.KeywordsSourcePrimaries)
                    .Where(keywordPrimary => keywordPrimary.Name.Equals(keyword.Name))
                    .SingleOrDefaultAsync();

                var primarySources = await _dbContext.SourcePrimaries.Where(x => primarySourceIds.Contains(x.Id)).ToListAsync();

                if (keywordPrimary != null)
                {
                    var existingKeywordSourceIds = keywordPrimary.KeywordsSourcePrimaries.Select(x => x.PrimarySourceId).ToHashSet();

                    primarySources
                        .Where(x => !existingKeywordSourceIds.Contains(x.Id))
                        .ToList()
                        .ForEach(source => source.KeywordsSourcePrimaries.Add(new Entities.KeywordsSourcePrimary
                        {
                            PrimarySource = source,
                            KeywordsPrimary = keywordPrimary
                        }));

                    validations.Add($"Keyword '{keyword.Name}' already exists");
                }
                else
                {
                    var keywordPrimaryEntity = await _dbContext.AddAsync(keyword.ToEntity());

                    primarySources
                        .ForEach(source => source.KeywordsSourcePrimaries.Add(new Entities.KeywordsSourcePrimary
                        {
                            PrimarySource = source,
                            KeywordsPrimary = keywordPrimaryEntity.Entity
                        }));
                }

                await _dbContext.SaveChangesAsync();
            }

            return validations;
        }

        public async Task DeleteAsync(long id)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries.FindAsync(id);
            if (keywordPrimary != null) _dbContext.KeywordsPrimaries.Remove(keywordPrimary);
        }

        public async Task<Keyword> GetAsync(long id)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries
                .Include(keywordsPrimaries => keywordsPrimaries.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .Where(keywordsPrimaries => keywordsPrimaries.Id == id)
                .SingleOrDefaultAsync();

            return keywordPrimary.ToDomain();
        }

        public async Task<IEnumerable<Keyword>> GetAllAsync()
        {
            var keywordsPrimary = await _dbContext.KeywordsPrimaries
                .Include(keywordsPrimaries => keywordsPrimaries.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .ToListAsync();

            return keywordsPrimary.ToDomain();
        }

        public async Task UpdateAsync(Keyword keyword)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries.FindAsync(keyword.Id);

            if (keywordPrimary != null)
            {
                keywordPrimary.Name = keyword.Name;
                keywordPrimary.Active = keyword.Active;
                keywordPrimary.ModifiedBy = keyword.ModifiedBy;
                keywordPrimary.ModifiedDate = keyword.ModifiedDate;
            }
        }

        public async Task<IEnumerable<Keyword>> SearchAsync(string keywordName)
        {
            var keywordsPrimaries = await _dbContext.KeywordsPrimaries
                .Include(keywordsPrimaries => keywordsPrimaries.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .Where(keywordPrimary => keywordPrimary.Name.Contains(keywordName))
                .ToListAsync();

            return keywordsPrimaries.ToDomain();
        }
    }
}

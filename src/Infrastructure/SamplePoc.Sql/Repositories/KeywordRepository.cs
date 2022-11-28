using Microsoft.EntityFrameworkCore;
using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;
using SamplePoc.Sql.Entities;
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

        public async Task AddAsync(Keyword keyword)
        {
            var primarySourceIds = keyword.PrimarySources.Select(x => x.Id).ToHashSet();
            var keywordPrimaryEntity = await _dbContext.AddAsync(keyword.ToEntity());
            var primarySources = await _dbContext.SourcePrimaries.Where(x => primarySourceIds.Contains(x.Id)).ToListAsync();

            primarySources.ForEach(source => source.KeywordsSourcePrimaries.Add(new Entities.KeywordsSourcePrimary
            {
                PrimarySource = source,
                KeywordsPrimary = keywordPrimaryEntity.Entity
            }));
        }

        public async Task BulkAddAsync(IEnumerable<Keyword> keywords)
        {
            var keywordAddTasks = keywords.Select(x => AddAsync(x));
            await Task.WhenAll(keywordAddTasks.ToArray());
        }

        public async Task DeleteAsync(long id)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries.FindAsync(id);
            if (keywordPrimary != null) _dbContext.KeywordsPrimaries.Remove(keywordPrimary);
        }

        public async Task<Keyword> GetAsync(long id)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries.Include(x => x.KeywordsSourcePrimaries).Where(x => x.Id == id).SingleOrDefaultAsync();

            if (keywordPrimary != null)
            {
                var primarySourceIds = keywordPrimary.KeywordsSourcePrimaries.Select(x => x.PrimarySourceId).ToHashSet();
                await _dbContext.KeywordsSourcePrimaries.Include(x => x.PrimarySource).Where(x => primarySourceIds.Contains(x.PrimarySourceId)).ToListAsync();
            }

            return keywordPrimary.ToDomain();
        }

        public async Task<IEnumerable<Keyword>> GetAllAsync()
        {
            var keywordsPrimary = await _dbContext.KeywordsPrimaries.Include(x => x.KeywordsSourcePrimaries).ToListAsync();

            foreach (var keywordPrimary in keywordsPrimary)
            {
                var primarySourceIds = keywordPrimary.KeywordsSourcePrimaries.Select(x => x.PrimarySourceId).ToHashSet();
                _dbContext.KeywordsSourcePrimaries.Include(x => x.PrimarySource).Where(x => primarySourceIds.Contains(x.PrimarySourceId)).ToList();
            }

            return keywordsPrimary.ToDomain();
        }

        public async Task UpdateAsync(Keyword keyword)
        {
            var keywordPrimary = await _dbContext.KeywordsPrimaries.FindAsync(keyword.Id);

            if (keywordPrimary != null )
            {
                keywordPrimary.Name = keyword.Name;
                keywordPrimary.Active = keyword.Active;
                keywordPrimary.ModifiedBy = keyword.ModifiedBy;
                keywordPrimary.ModifiedDate = keyword.ModifiedDate;
            }
        }
    }
}

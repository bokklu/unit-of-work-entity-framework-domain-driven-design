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

        public async Task AddAsync(Keyword keyword)
        {
            var keywordPrimaryEntity = await _dbContext.AddAsync(keyword.ToEntity());
            var primarySources = _dbContext.SourcePrimaries.Where(x => keyword.PrimarySources.Contains(x.Id)).ToList();

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
            var keywordPrimary = await _dbContext.KeywordsPrimaries.FindAsync(id);

            if (keywordPrimary != null)
            {
                var keywordSourcePrimaryIds = _dbContext.KeywordsSourcePrimaries.Where(x => x.KeywordsPrimaryId == id).ToList();
                keywordPrimary.KeywordsSourcePrimaries = keywordSourcePrimaryIds;
            }

            return keywordPrimary.ToDomain();
        }

        public Task<IEnumerable<Keyword>> GetAllAsync()
        {
            var keywordsPrimary = _dbContext.KeywordsPrimaries.ToList();

            keywordsPrimary.ForEach(kp =>
            {
                var sourcePrimaries = _dbContext.KeywordsSourcePrimaries.Where(sp => sp.KeywordsPrimaryId == kp.Id).ToList();
                kp.KeywordsSourcePrimaries = sourcePrimaries;
            });

            return Task.FromResult(keywordsPrimary.ToDomain());
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

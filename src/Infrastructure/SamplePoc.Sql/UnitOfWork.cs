using SamplePoc.Services.Abstraction;
using SamplePoc.Sql.Repositories;

namespace SamplePoc.Sql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketingSuiteContext _dbContext;
        private ICampaignRepository _campaignRepository;
        private IKeywordRepository _keywordRepository;
        private IClientRepository _clientRepository;

        public UnitOfWork(MarketingSuiteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICampaignRepository CampaignRepository { get { return _campaignRepository ??= new CampaignRepository(_dbContext); } }

        public IKeywordRepository KeywordRepository { get { return _keywordRepository ??= new KeywordRepository(_dbContext); } }

        public IClientRepository ClientRepository { get { return _clientRepository ??= new ClientRepository(_dbContext); } }

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync() => await _dbContext.DisposeAsync();
    }
}

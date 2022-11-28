using Microsoft.EntityFrameworkCore;
using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;
using SamplePoc.Sql.Extensions;

namespace SamplePoc.Sql.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MarketingSuiteContext _dbContext;

        public ClientRepository(MarketingSuiteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetAsync(int id)
        {
            var clientEntity = await _dbContext.Clients
                .Include(clients => clients.ClientsCampaigns)
                .ThenInclude(clientsCampaigns => clientsCampaigns.Campaign)
                .ThenInclude(campaign => campaign.CampaignsKeywords)
                .ThenInclude(campaignsKeywords => campaignsKeywords.KeywordsPrimary)
                .ThenInclude(keywordsPrimary => keywordsPrimary.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .Where(client => client.Id == id)
                .SingleOrDefaultAsync();

            return clientEntity.ToDomain();
        }
    }
}

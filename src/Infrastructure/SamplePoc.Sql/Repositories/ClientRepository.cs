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
            var clientEntity = await _dbContext.Clients.Include(c => c.ClientsCampaigns).Where(x => x.Id == id).SingleOrDefaultAsync();

            if (clientEntity != null)
            {
                var campaignIds = clientEntity.ClientsCampaigns.Select(x => x.CampaignId).ToHashSet();
                var campaigns = await _dbContext.Campaigns.Include(c => c.CampaignsKeywords).Where(c => campaignIds.Contains(c.Id)).ToListAsync();

                foreach (var campaign in campaigns)
                {
                    var keywordIds = campaign.CampaignsKeywords.Select(x => x.KeywordsPrimaryId).ToHashSet();
                    var campaignsKeywords = await _dbContext.CampaignsKeywords.Include(x => x.KeywordsPrimary).Where(x => keywordIds.Contains(x.KeywordsPrimaryId)).ToListAsync();
                    var keywordPrimaries = await _dbContext.KeywordsPrimaries.Include(x => x.KeywordsSourcePrimaries).Where(x => keywordIds.Contains(x.Id)).ToListAsync();
                    await _dbContext.KeywordsSourcePrimaries.Include(x => x.PrimarySource).Where(x => keywordIds.Contains(x.KeywordsPrimaryId)).ToListAsync();
                }
            }

            return clientEntity.ToDomain();
        }
    }
}

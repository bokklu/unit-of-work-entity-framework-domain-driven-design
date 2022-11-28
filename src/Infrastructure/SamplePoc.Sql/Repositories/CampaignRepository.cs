using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;
using SamplePoc.Sql.Extensions;

namespace SamplePoc.Sql.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly MarketingSuiteContext _dbContext;

        public CampaignRepository(MarketingSuiteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Campaign campaign)
        {
            var campaignEntity = await _dbContext.Campaigns.AddAsync(campaign.ToEntity());
            var keywordsPrimary = _dbContext.KeywordsPrimaries.Where(x => campaign.KeywordIds.Contains(x.Id)).ToList();
            
            keywordsPrimary.ForEach(keyword => keyword.CampaignsKeywords.Add(new Entities.CampaignsKeyword 
            { 
                KeywordsPrimary = keyword, 
                Campaign = campaignEntity.Entity 
            }));
        }

        public async Task BulkAddAsync(IEnumerable<Campaign> campaigns)
        {
            var bulkAddTasks = campaigns.Select(x => AddAsync(x));
            await Task.WhenAll(bulkAddTasks.ToArray());
        }

        public async Task DeleteAsync(int id)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);
            if (campaign != null) _dbContext.Campaigns.Remove(campaign);
        }

        public async Task UpdateAsync(Campaign campaign)
        {
            var campaignEntity = await _dbContext.Campaigns.FindAsync(campaign.Id);

            if (campaignEntity != null)
            {
                campaignEntity.Name = campaign.Name;
                campaignEntity.Description = campaign.Description;
                campaignEntity.Active = campaign.Active;
                campaignEntity.ModifiedDate = campaign.ModifiedDate;
                campaignEntity.ModifiedBy = campaign.ModifiedBy;
            }
        }

        public async Task<Campaign> GetAsync(int id)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(id);

            if (campaign != null)
            {
                var keywordCampaignIds = _dbContext.CampaignsKeywords.Where(x => x.CampaignId == id).ToList();
                campaign.CampaignsKeywords = keywordCampaignIds;
            }

            return campaign.ToDomain();
        }

        public Task<IEnumerable<Campaign>> GetAllAsync()
        {
            var campaigns = _dbContext.Campaigns.ToList();

            campaigns.ForEach(c => {
                var campaignKeywords = _dbContext.CampaignsKeywords.Where(ck => ck.CampaignId == c.Id).ToList();
                c.CampaignsKeywords = campaignKeywords;
            });

            return Task.FromResult(campaigns.ToDomain());
        }
    }
}

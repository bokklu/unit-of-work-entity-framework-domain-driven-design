using Microsoft.EntityFrameworkCore;
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
            var keywordIds = campaign.Keywords.Select(x => x.Id).ToHashSet();
            var campaignEntity = await _dbContext.Campaigns.AddAsync(campaign.ToEntity());
            var keywordsPrimary = await _dbContext.KeywordsPrimaries.Where(x => keywordIds.Contains(x.Id)).ToListAsync();
            
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
            var campaign = await _dbContext.Campaigns
                .Include(campaign => campaign.CampaignsKeywords)
                .ThenInclude(campaignKeywords => campaignKeywords.KeywordsPrimary)
                .ThenInclude(keywordsPrimary => keywordsPrimary.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .Where(campaign => campaign.Id == id)
                .SingleOrDefaultAsync();

            return campaign.ToDomain();
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            var campaigns = await _dbContext.Campaigns
                .Include(campaigns => campaigns.CampaignsKeywords)
                .ThenInclude(campaignsKeywords => campaignsKeywords.KeywordsPrimary)
                .ThenInclude(keywordsPrimary => keywordsPrimary.KeywordsSourcePrimaries)
                .ThenInclude(keywordsSourcePrimaries => keywordsSourcePrimaries.PrimarySource)
                .ToListAsync();

            return campaigns.ToDomain();
        }
    }
}

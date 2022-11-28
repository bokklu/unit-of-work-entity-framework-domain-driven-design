using SamplePoc.Contracts.Request;

namespace SamplePoc.Services.Extensions
{
    public static class RequestToDomainConverter
    {
        public static Domain.Keyword ToDomain(this KeywordAddRequest keyword) 
            => Domain.Keyword.Create(default, keyword.Name, keyword.ModifiedDate, keyword.ModifiedBy, keyword.Active, keyword.PrimarySourceIds.ToHashSet());

        public static IEnumerable<Domain.Keyword> ToDomain(this IEnumerable<KeywordAddRequest> keywords)
            => keywords.Select(x => x.ToDomain()).ToList();

        public static Domain.Keyword ToDomain(this KeywordUpdateRequest keyword)
            => Domain.Keyword.Create(keyword.Id, keyword.Name, keyword.ModifiedDate, keyword.ModifiedBy, keyword.Active, new HashSet<short>());

        public static Domain.Campaign ToDomain(this CampaignAddRequest campaign) 
            => Domain.Campaign.Create(default, campaign.Name, campaign.Description, campaign.Active, campaign.ModifiedDate,
                campaign.ModifiedBy, campaign.KeywordIds.ToHashSet());

        public static IEnumerable<Domain.Campaign> ToDomain(this IEnumerable<CampaignAddRequest> campaigns)
            => campaigns.Select(x => x.ToDomain()).ToList();

        public static Domain.Campaign ToDomain(this CampaignUpdateRequest campaign) 
            => Domain.Campaign.Create(campaign.Id, campaign.Name, campaign.Description, campaign.Active, campaign.ModifiedDate, campaign.ModifiedBy, new HashSet<long>());
    }
}

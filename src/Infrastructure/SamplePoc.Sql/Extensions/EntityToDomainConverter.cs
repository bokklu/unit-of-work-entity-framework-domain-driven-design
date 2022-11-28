namespace SamplePoc.Sql.Extensions
{
    public static class EntityToDomainConverter
    {
        public static Domain.Campaign ToDomain(this Entities.Campaign campaign)
        {
            if (campaign == null) return null;

            var campaignDomain = Domain.Campaign.Create(campaign.Id, campaign.Name, campaign.Description,
                campaign.Active, campaign.ModifiedDate, campaign.ModifiedBy, campaign.CampaignsKeywords.Select(x => x.KeywordsPrimary.ToDomain()));

            return campaignDomain;
        }

        public static IEnumerable<Domain.Campaign> ToDomain(this IEnumerable<Entities.Campaign> campaigns)
            => campaigns.Select(x => x.ToDomain());

        public static Domain.Keyword ToDomain(this Entities.KeywordsPrimary keyword)
        {
            if (keyword == null) return null;

            var keywordDomain = Domain.Keyword.Create(keyword.Id, keyword.Name, keyword.ModifiedDate, keyword.ModifiedBy,
                keyword.Active, keyword.KeywordsSourcePrimaries.Select(x => x.PrimarySource.ToDomain()));

            return keywordDomain;
        }

        public static Domain.KeywordSource ToDomain(this Entities.SourcePrimary primarySource)
        {
            if (primarySource == null) return null;

            return Domain.KeywordSource.Create(primarySource.Id, primarySource.Name, primarySource.Description,
                primarySource.Url, primarySource.ModifiedDate, primarySource.ModifiedBy, primarySource.Active);
        }

        public static IEnumerable<Domain.Keyword> ToDomain(this IEnumerable<Entities.KeywordsPrimary> keywords)
            => keywords.Select(x => x.ToDomain());

        public static Domain.Client ToDomain(this Entities.Client client)
        {
            if (client == null) return null;

            return Domain.Client.Create(client.Id, client.Name, client.Description, client.Url, client.ModifiedDate,
                client.ModifiedBy, client.Active, client.ClientsCampaigns.Select(x => x.Campaign.ToDomain()));
        }
    }
}

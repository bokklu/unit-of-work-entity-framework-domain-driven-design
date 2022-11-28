namespace SamplePoc.Services.Extensions
{
    public static class DomainToResponseConverter
    {
        public static Contracts.Response.CampaignResponse ToResponse(this Domain.Campaign campaign)
        {
            if (campaign == null) return null;

            return new Contracts.Response.CampaignResponse
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                Active = campaign.Active,
                ModifiedBy = campaign.ModifiedBy,
                ModifiedDate = campaign.ModifiedDate,
                KeywordIds = campaign.KeywordIds
            };
        }

        public static IEnumerable<Contracts.Response.CampaignResponse> ToResponse(this IEnumerable<Domain.Campaign> campaigns)
            => campaigns.Select(x => x.ToResponse());

        public static Contracts.Response.KeywordResponse ToResponse(this Domain.Keyword keyword)
        {
            if (keyword == null) return null;

            return new Contracts.Response.KeywordResponse
            {
                Id = keyword.Id,
                Name = keyword.Name,
                ModifiedBy = keyword.ModifiedBy,
                ModifiedDate = keyword.ModifiedDate,
                Active = keyword.Active,
                PrimarySources = keyword.PrimarySources
            };
        }

        public static IEnumerable<Contracts.Response.KeywordResponse> ToResponse(this IEnumerable<Domain.Keyword> keywords)
            => keywords.Select(x => x.ToResponse());
    }
}

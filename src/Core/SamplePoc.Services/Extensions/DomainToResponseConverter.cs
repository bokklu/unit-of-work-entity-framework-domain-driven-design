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
                Keywords = campaign.Keywords.ToResponse()
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
                PrimarySources = keyword.PrimarySources.ToResponse()
            };
        }

        public static Contracts.Response.KeywordSourceResponse ToResponse(this Domain.KeywordSource keywordSource)
        {
            if (keywordSource == null) return null;

            return new Contracts.Response.KeywordSourceResponse
            {
                Id = keywordSource.Id,
                Name = keywordSource.Name,
                Description = keywordSource.Description,
                Active = keywordSource.Active,
                Url = keywordSource.Url,
                ModifiedBy = keywordSource.ModifiedBy,
                ModifiedDate = keywordSource.ModifiedDate
            };
        }

        public static IEnumerable<Contracts.Response.KeywordSourceResponse> ToResponse(this IEnumerable<Domain.KeywordSource> keywordSources)
            => keywordSources.Select(x => x.ToResponse());

        public static IEnumerable<Contracts.Response.KeywordResponse> ToResponse(this IEnumerable<Domain.Keyword> keywords)
            => keywords.Select(x => x.ToResponse());
    }
}

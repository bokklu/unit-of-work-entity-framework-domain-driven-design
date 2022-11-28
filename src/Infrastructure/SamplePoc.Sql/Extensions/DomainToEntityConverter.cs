namespace SamplePoc.Sql.Extensions
{
    public static class DomainToEntityConverter
    {
        public static Entities.Campaign ToEntity(this Domain.Campaign campaign)
        {
            if (campaign == null) return null;

            return new Entities.Campaign
            {
                Name = campaign.Name,
                Description = campaign.Description,
                Active = campaign.Active,
                ModifiedDate = campaign.ModifiedDate,
                ModifiedBy = campaign.ModifiedBy
            };
        }

        public static Entities.KeywordsPrimary ToEntity(this Domain.Keyword keyword)
        {
            if (keyword == null) return null;

            return new Entities.KeywordsPrimary 
            {
                Name = keyword.Name,
                ModifiedDate= keyword.ModifiedDate,
                ModifiedBy= keyword.ModifiedBy,
                Active = keyword.Active
            };
        }
    }
}

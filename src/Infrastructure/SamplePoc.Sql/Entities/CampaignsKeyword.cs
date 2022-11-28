namespace SamplePoc.Sql.Entities;

public partial class CampaignsKeyword
{
    public long Id { get; set; }

    public int CampaignId { get; set; }

    public long KeywordsPrimaryId { get; set; }

    public virtual Campaign Campaign { get; set; }

    public virtual KeywordsPrimary KeywordsPrimary { get; set; }
}

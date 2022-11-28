namespace SamplePoc.Sql.Entities;

public partial class Campaign
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public virtual ICollection<CampaignsKeyword> CampaignsKeywords { get; set; } = new List<CampaignsKeyword>();

    public virtual ICollection<ClientsCampaign> ClientsCampaigns { get; set; } = new List<ClientsCampaign>();
}

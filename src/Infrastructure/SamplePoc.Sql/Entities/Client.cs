namespace SamplePoc.Sql.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ClientsCampaign> ClientsCampaigns { get; set; } = new List<ClientsCampaign>();
}

namespace SamplePoc.Sql.Entities;

public partial class ClientsCampaign
{
    public int Id { get; set; }

    public int CampaignId { get; set; }

    public int ClientId { get; set; }

    public virtual Campaign Campaign { get; set; }

    public virtual Client Client { get; set; }
}

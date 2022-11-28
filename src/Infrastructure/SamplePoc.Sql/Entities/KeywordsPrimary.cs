using System.ComponentModel.DataAnnotations.Schema;

namespace SamplePoc.Sql.Entities;

public partial class KeywordsPrimary
{
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<CampaignsKeyword> CampaignsKeywords { get; set; } = new List<CampaignsKeyword>();

    public virtual ICollection<KeywordsSourcePrimary> KeywordsSourcePrimaries { get; set; } = new List<KeywordsSourcePrimary>();
}

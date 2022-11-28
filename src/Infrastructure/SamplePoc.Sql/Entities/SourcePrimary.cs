using System.ComponentModel.DataAnnotations.Schema;

namespace SamplePoc.Sql.Entities;

public partial class SourcePrimary
{
    public short Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<KeywordsSourcePrimary> KeywordsSourcePrimaries { get; set; } = new List<KeywordsSourcePrimary>();
}

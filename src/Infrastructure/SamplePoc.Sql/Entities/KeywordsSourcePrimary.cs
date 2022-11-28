namespace SamplePoc.Sql.Entities;

public partial class KeywordsSourcePrimary
{
    public long Id { get; set; }

    public long KeywordsPrimaryId { get; set; }

    public short PrimarySourceId { get; set; }

    public virtual KeywordsPrimary KeywordsPrimary { get; set; }

    public virtual SourcePrimary PrimarySource { get; set; }
}

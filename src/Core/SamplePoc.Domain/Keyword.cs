namespace SamplePoc.Domain
{
    public class Keyword
    {
        public long Id { get; }
        public string Name { get; }
        public DateTime ModifiedDate { get; }
        public string ModifiedBy { get; }
        public bool Active { get; }
        public IEnumerable<KeywordSource> PrimarySources { get; }

        private Keyword(long id, string name, DateTime modifiedDate, string modifiedBy, bool active, IEnumerable<KeywordSource> primarySources)
        {
            Id = id;
            Name = name;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            Active = active;
            PrimarySources = primarySources;
        }

        public static Keyword Create(long id, string name, DateTime modifiedDate, string modifiedBy, bool active, IEnumerable<KeywordSource> primarySources) 
            => new(id, name, modifiedDate, modifiedBy, active, primarySources);

        public static Keyword CreateFromRequest(long id, string name, DateTime modifiedDate, string modifiedBy, bool active, IEnumerable<short> primarySourceIds)
            => new(id, name, modifiedDate, modifiedBy, active, primarySourceIds.Select(KeywordSource.CreateFromRequest));

        public static Keyword CreateFromId(long id)
            => new(id, default, default, default, default, Enumerable.Empty<KeywordSource>());

        #region Domain Methods
        #endregion
    }
}

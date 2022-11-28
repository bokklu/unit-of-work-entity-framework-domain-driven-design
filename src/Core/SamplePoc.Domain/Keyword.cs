namespace SamplePoc.Domain
{
    public class Keyword
    {
        public long Id { get; }
        public string Name { get; }
        public DateTime ModifiedDate { get; }
        public string ModifiedBy { get; }
        public bool Active { get; }
        public ISet<short> PrimarySources { get; }

        private Keyword(long id, string name, DateTime modifiedDate, string modifiedBy, bool active, ISet<short> primarySources)
        {
            Id = id;
            Name = name;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            Active = active;
            PrimarySources = primarySources;
        }

        public static Keyword Create(long id, string name, DateTime modifiedDate, string modifiedBy, bool active, ISet<short> primarySources) 
            => new(id, name, modifiedDate, modifiedBy, active, primarySources);
    }
}

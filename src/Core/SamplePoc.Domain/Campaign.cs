namespace SamplePoc.Domain
{
    public class Campaign
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Active { get; }
        public DateTime ModifiedDate { get; }
        public string ModifiedBy { get; }
        public IEnumerable<Keyword> Keywords { get; }

        private Campaign(int id, string name, string description, bool active, DateTime modifiedDate, string modifiedBy, IEnumerable<Keyword> keywords)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            Keywords = keywords;
        }

        public static Campaign Create(int id, string name, string description, bool active, DateTime modifiedDate, string modifiedBy, IEnumerable<Keyword> keywords)
            => new(id, name, description, active, modifiedDate, modifiedBy, keywords);

        public static Campaign CreateFromRequest(int id, string name, string description, bool active, DateTime modifiedDate, string modifiedBy, IEnumerable<long> keywordIds)
            => new(id, name, description, active, modifiedDate, modifiedBy, keywordIds.Select(Keyword.CreateFromId));

        #region Include Domain Mutators here
        #endregion
    }
}
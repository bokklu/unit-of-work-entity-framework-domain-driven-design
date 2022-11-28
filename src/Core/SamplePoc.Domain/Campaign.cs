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
        public ISet<long> KeywordIds { get; }

        private Campaign(int id, string name, string description, bool active, DateTime modifiedDate, string modifiedBy, ISet<long> keywordIds)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            KeywordIds = keywordIds;
        }

        public static Campaign Create(int id, string name, string description, bool active, DateTime modifiedDate, string modifiedBy, ISet<long> keywordIds)
            => new(id, name, description, active, modifiedDate, modifiedBy, keywordIds);

        #region Include Domain Mutators here
        #endregion
    }
}
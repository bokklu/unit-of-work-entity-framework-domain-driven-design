namespace SamplePoc.Domain
{
    public class KeywordSource
    {
        public short Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Url { get; }
        public DateTime ModifiedDate { get; }
        public string ModifiedBy { get; }
        public bool Active { get; }

        private KeywordSource(short id, string name, string description, string url, DateTime modifiedDate, string modifiedBy, bool active)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            Active = active;
        }

        public static KeywordSource Create(short id, string name, string description, string url, DateTime modifiedDate, string modifiedBy, bool active)
            => new(id, name, description, url, modifiedDate, modifiedBy, active);

        public static KeywordSource CreateFromRequest(short id)
            => new(id, default, default, default, default, default, default);

        #region Domain Methods
        #endregion
    }
}

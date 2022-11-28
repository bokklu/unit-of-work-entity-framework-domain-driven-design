namespace SamplePoc.Domain
{
    public class Client
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Url { get; }
        public DateTime ModifiedDate { get; }
        public string ModifiedBy { get; }
        public bool Active { get; }
        public IEnumerable<Campaign> Campaigns { get; }

        private Client(int id, string name, string description, string url, DateTime modifiedDate, string modifiedBy, bool active, IEnumerable<Campaign> campaigns)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            Active = active;
            Campaigns = campaigns;
        }

        public static Client Create(int id, string name, string description, string url, DateTime modifiedDate, string modifiedBy, bool active, IEnumerable<Campaign> campaigns)
            => new(id, name, description, url, modifiedDate, modifiedBy, active, campaigns);
    }
}

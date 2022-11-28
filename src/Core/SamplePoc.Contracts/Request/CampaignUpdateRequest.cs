namespace SamplePoc.Contracts.Request
{
    public class CampaignUpdateRequest
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool Active { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string ModifiedBy { get; init; }
    }
}

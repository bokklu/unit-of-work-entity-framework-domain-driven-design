namespace SamplePoc.Contracts.Response
{
    public class KeywordSourceResponse
    {
        public short Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Url { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string ModifiedBy { get; init; }
        public bool Active { get; init; }
    }
}

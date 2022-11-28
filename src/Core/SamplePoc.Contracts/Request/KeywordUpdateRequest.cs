namespace SamplePoc.Contracts.Request
{
    public class KeywordUpdateRequest
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string ModifiedBy { get; init; }
        public bool Active { get; init; }
    }
}

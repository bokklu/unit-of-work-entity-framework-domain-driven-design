namespace SamplePoc.Contracts.Request
{
    public class KeywordAddRequest
    {
        public string Name { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string ModifiedBy { get; init; }
        public bool Active { get; init; }
        public IEnumerable<short> PrimarySourceIds { get; init; }
    }
}

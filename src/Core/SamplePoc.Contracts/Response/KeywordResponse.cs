namespace SamplePoc.Contracts.Response
{
    public class KeywordResponse
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string ModifiedBy { get; init; }
        public bool Active { get; init; }
        public IEnumerable<KeywordSourceResponse> PrimarySources { get; init; }
    }
}

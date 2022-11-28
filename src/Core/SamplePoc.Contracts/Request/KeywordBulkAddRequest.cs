namespace SamplePoc.Contracts.Request
{
    public class KeywordBulkAddRequest
    {
        public IEnumerable<KeywordAddRequest> Keywords { get; init; }
    }
}

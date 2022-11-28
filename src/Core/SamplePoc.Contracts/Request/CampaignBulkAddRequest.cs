namespace SamplePoc.Contracts.Request
{
    public class CampaignBulkAddRequest
    {
        public IEnumerable<CampaignAddRequest> Campaigns { get; init; }
    }
}

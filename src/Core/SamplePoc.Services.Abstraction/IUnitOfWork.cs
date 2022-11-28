namespace SamplePoc.Services.Abstraction
{
    public interface IUnitOfWork
    {
        ICampaignRepository CampaignRepository { get; }
        IKeywordRepository KeywordRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}

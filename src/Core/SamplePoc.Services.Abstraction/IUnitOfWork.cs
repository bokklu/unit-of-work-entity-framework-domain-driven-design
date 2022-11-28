namespace SamplePoc.Services.Abstraction
{
    public interface IUnitOfWork
    {
        ICampaignRepository CampaignRepository { get; }
        IKeywordRepository KeywordRepository { get; }
        IClientRepository ClientRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}

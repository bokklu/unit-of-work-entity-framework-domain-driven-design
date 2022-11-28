using SamplePoc.Domain;

namespace SamplePoc.Services.Abstraction
{
    public interface ICampaignRepository
    {
        Task AddAsync(Campaign campaign);
        Task BulkAddAsync(IEnumerable<Campaign> campaigns);
        Task DeleteAsync(int id);
        Task UpdateAsync(Campaign campaign);
        Task<Campaign> GetAsync(int id);
        Task<IEnumerable<Campaign>> GetAllAsync();
    }
}

using SamplePoc.Domain;

namespace SamplePoc.Services.Abstraction
{
    public interface IKeywordRepository
    {
        Task DeleteAsync(long id);
        Task AddAsync(Keyword keyword);
        Task BulkAddAsync(IEnumerable<Keyword> keywords);
        Task<Keyword> GetAsync(long id);
        Task<IEnumerable<Keyword>> GetAllAsync();
        Task UpdateAsync(Keyword keyword);
    }
}

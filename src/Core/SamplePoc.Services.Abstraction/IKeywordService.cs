using SamplePoc.Domain;

namespace SamplePoc.Services.Abstraction
{
    public interface IKeywordService
    {
        Task DeleteAsync(long id);
        Task<bool> AddAsync(Keyword keyword);
        Task BulkAddAsync(IEnumerable<Keyword> keywords);
        Task<Keyword> GetAsync(long id);
        Task<IEnumerable<Keyword>> GetAllAsync();
        Task UpdateAsync(Keyword keyword);
        Task<IEnumerable<Keyword>> SearchAsync(string keywordName);
    }
}

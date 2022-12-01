using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;

namespace SamplePoc.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KeywordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(long id)
        {
            await _unitOfWork.KeywordRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> AddAsync(Keyword keyword)
        {
            var keywordExists = await _unitOfWork.KeywordRepository.AddAsync(keyword);
            await _unitOfWork.CommitAsync();
            return keywordExists;
        }

        public async Task BulkAddAsync(IEnumerable<Keyword> keywords)
        {
            await _unitOfWork.KeywordRepository.BulkAddAsync(keywords);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Keyword> GetAsync(long id)
        {
            var maybeKeyword = await _unitOfWork.KeywordRepository.GetAsync(id);
            await _unitOfWork.CommitAsync();
            return maybeKeyword;
        }

        public async Task<IEnumerable<Keyword>> GetAllAsync()
        {
            var maybeKeywords = await _unitOfWork.KeywordRepository.GetAllAsync();
            await _unitOfWork.CommitAsync();
            return maybeKeywords;
        }

        public async Task UpdateAsync(Keyword keyword)
        {
            await _unitOfWork.KeywordRepository.UpdateAsync(keyword);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Keyword>> SearchAsync(string keywordName)
        {
            var maybeKeywords = await _unitOfWork.KeywordRepository.SearchAsync(keywordName);
            await _unitOfWork.CommitAsync();
            return maybeKeywords;
        }
    }
}

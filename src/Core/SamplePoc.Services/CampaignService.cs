using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;

namespace SamplePoc.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CampaignService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Campaign campaign)
        {
            await _unitOfWork.CampaignRepository.AddAsync(campaign);
            await _unitOfWork.CommitAsync();
        }

        public async Task BulkAddAsync(IEnumerable<Campaign> campaigns)
        {
            await _unitOfWork.CampaignRepository.BulkAddAsync(campaigns);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.CampaignRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Campaign campaign)
        {
            await _unitOfWork.CampaignRepository.UpdateAsync(campaign);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Campaign> GetAsync(int id)
        {
            var maybeCampaign = await _unitOfWork.CampaignRepository.GetAsync(id);
            await _unitOfWork.CommitAsync();
            return maybeCampaign;
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            var maybeCampaigns = await _unitOfWork.CampaignRepository.GetAllAsync();
            await _unitOfWork.CommitAsync();
            return maybeCampaigns.ToList();
        }
    }
}
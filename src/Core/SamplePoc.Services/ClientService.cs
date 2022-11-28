using SamplePoc.Domain;
using SamplePoc.Services.Abstraction;

namespace SamplePoc.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Client> GetAsync(int id)
        {
            var maybeClient = await _unitOfWork.ClientRepository.GetAsync(id);
            await _unitOfWork.CommitAsync();
            return maybeClient;
        }
    }
}

using SamplePoc.Domain;

namespace SamplePoc.Services.Abstraction
{
    public interface IClientRepository
    {
        Task<Client> GetAsync(int id);
    }
}

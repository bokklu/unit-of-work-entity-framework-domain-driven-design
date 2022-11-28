using SamplePoc.Domain;

namespace SamplePoc.Services.Abstraction
{
    public interface IClientService
    {
        Task<Client> GetAsync(int id);
    }
}

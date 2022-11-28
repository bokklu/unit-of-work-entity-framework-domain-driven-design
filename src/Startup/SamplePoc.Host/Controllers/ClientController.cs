using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamplePoc.Services.Abstraction;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace SamplePoc.Host.Controllers
{
    [Route("client/[action]")]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ActionName("get")]
        [SwaggerOperation("Gets the client info by Id with campaign and related keywords")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> GetClientInfoAsync(int id)
        {
            return HandleRequestAsync(async () =>
            {
                if (id <= 0) return BadRequest();
                var maybeClient = await _clientService.GetAsync(id);
                return maybeClient == null ? NotFound() : Ok(maybeClient);
            });
        }
    }
}

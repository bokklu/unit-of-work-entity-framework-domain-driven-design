using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace SamplePoc.Host.Controllers
{
    [Route("client/[action]")]
    public class ClientController : BaseController
    {
        [HttpGet]
        [ActionName("")]
        [SwaggerOperation("Gets the client info by Id with campaign and related keywords")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> GetClientInfoAsync(int id)
        {
            return HandleRequestAsync(async () =>
            {
                return await Task.FromResult(Ok());
            });
        }
    }
}

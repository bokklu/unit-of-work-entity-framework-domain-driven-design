using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SamplePoc.Host.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> fn)
        {
            try
            {
                return await fn();
            }
            catch (Exception)
            {
                //log exception here

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

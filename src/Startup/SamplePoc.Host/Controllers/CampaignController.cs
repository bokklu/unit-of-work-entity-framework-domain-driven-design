using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamplePoc.Contracts.Request;
using SamplePoc.Contracts.Response;
using SamplePoc.Services.Abstraction;
using SamplePoc.Services.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplePoc.Host.Controllers
{
    [Route("campaign/[action]")]
    public class CampaignController : BaseController
    {
        private readonly ICampaignService _campaignService;
        private readonly IValidator<CampaignAddRequest> _campaignAddValidator;
        private readonly IValidator<CampaignBulkAddRequest> _campaignBulkAddValidator;
        private readonly IValidator<CampaignUpdateRequest> _campaignUpdateValidator;

        public CampaignController(ICampaignService campaignService, IValidator<CampaignAddRequest> campaignAddValidator, 
            IValidator<CampaignBulkAddRequest> campaignBulkAddValidator, IValidator<CampaignUpdateRequest> campaignUpdateValidator)
        {
            _campaignService = campaignService;
            _campaignAddValidator = campaignAddValidator;
            _campaignBulkAddValidator = campaignBulkAddValidator;
            _campaignUpdateValidator = campaignUpdateValidator;
        }

        [HttpDelete]
        [ActionName("delete")]
        [SwaggerOperation("Deletes a campaign entity by id.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Campaign deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> DeleteAsync(int id)
        {
            return HandleRequestAsync(async () =>
            {
                if (id <= 0) return BadRequest();
                await _campaignService.DeleteAsync(id);
                return NoContent();
            });
        }

        [HttpPost]
        [ActionName("add")]
        [SwaggerOperation("Adds a campaign entity.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Campaign added successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> AddAsync([FromBody] CampaignAddRequest campaign)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _campaignAddValidator.Validate(campaign);

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                await _campaignService.AddAsync(campaign.ToDomain());
                return NoContent();
            });
        }

        [HttpPost]
        [ActionName("bulk-add")]
        [SwaggerOperation("Bulk adds a campaign entity.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Campaigns added successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> BulkAddAsync([FromBody] IEnumerable<CampaignAddRequest> campaigns)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _campaignBulkAddValidator.Validate(new CampaignBulkAddRequest { Campaigns = campaigns });

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                await _campaignService.BulkAddAsync(campaigns.ToDomain());
                return NoContent();
            });
        }

        [HttpPut]
        [ActionName("update")]
        [SwaggerOperation("Updates a campaign entity.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Campaign updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> UpdateAsync([FromBody] CampaignUpdateRequest campaign)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _campaignUpdateValidator.Validate(campaign);

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                await _campaignService.UpdateAsync(campaign.ToDomain());
                return await Task.FromResult(NoContent());
            });
        }

        [HttpGet]
        [ActionName("bulk-get")]
        [SwaggerOperation("Retrieves all campaign entities.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Campaigns retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entities not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> GetAllAsync()
        {
            return HandleRequestAsync(async () =>
            {
                var maybeCampaigns = await _campaignService.GetAllAsync();
                return maybeCampaigns.Any() ? Ok(maybeCampaigns.ToResponse()) : NoContent();
            });
        }

        [HttpGet]
        [ActionName("get")]
        [SwaggerOperation("Retrieve campaign entity by Id.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Campaign retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> GetAsync(int id)
        {
            return HandleRequestAsync(async () =>
            {
                if (id <= 0) return BadRequest();
                var maybeCampaign = await _campaignService.GetAsync(id);
                return maybeCampaign == null ? NotFound() : Ok(maybeCampaign.ToResponse());
            });
        }
    }
}

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
    [Route("keyword/[action]")]
    public class KeywordController : BaseController
    {
        private readonly IKeywordService _keywordService;
        private readonly IValidator<KeywordAddRequest> _keywordAddValidator;
        private readonly IValidator<KeywordBulkAddRequest> _keywordBulkAddValidator;
        private readonly IValidator<KeywordUpdateRequest> _keywordUpdateValidator;

        public KeywordController(IKeywordService keywordService, IValidator<KeywordAddRequest> keywordAddValidator, 
            IValidator<KeywordBulkAddRequest> keywordBulkAddValidator, IValidator<KeywordUpdateRequest> keywordUpdateValidator)
        {
            _keywordService = keywordService;
            _keywordAddValidator = keywordAddValidator;
            _keywordBulkAddValidator = keywordBulkAddValidator;
            _keywordUpdateValidator = keywordUpdateValidator;
        }

        [HttpDelete]
        [ActionName("delete")]
        [SwaggerOperation("Deletes a keyword entity by id.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Keyword deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> DeleteAsync(long id)
        {
            return HandleRequestAsync(async () =>
            {
                if (id <= 0) return BadRequest();
                await _keywordService.DeleteAsync(id);
                return NoContent();
            });
        }

        [HttpPost]
        [ActionName("add")]
        [SwaggerOperation("Adds a keyword entity.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keyword added successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> AddAsync([FromBody] KeywordAddRequest keyword)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _keywordAddValidator.Validate(keyword);

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                var keywordExists = await _keywordService.AddAsync(keyword.ToDomain());
                return keywordExists ? BadRequest(new ValidationResponse { Messages = new List<string>{"Keyword already exists"}}) : NoContent();
            });
        }

        [HttpPost]
        [ActionName("bulk-add")]
        [SwaggerOperation("Bulk adds a keyword entity.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keywords added successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> BulkAddAsync([FromBody] IEnumerable<KeywordAddRequest> keywords)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _keywordBulkAddValidator.Validate(new KeywordBulkAddRequest { Keywords = keywords});

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                var validations = await _keywordService.BulkAddAsync(keywords.ToDomain());
                return validations.Any() ? BadRequest(new ValidationResponse { Messages = validations }) : NoContent();
            });
        }

        [HttpPut]
        [ActionName("update")]
        [SwaggerOperation("Updates a keyword entity.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keyword updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> UpdateAsync([FromBody] KeywordUpdateRequest keyword)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _keywordUpdateValidator.Validate(keyword);

                if (!validationResult.IsValid)
                {
                    var validationErrors = new ValidationResponse { Messages = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return BadRequest(validationErrors);
                }

                await _keywordService.UpdateAsync(keyword.ToDomain());
                return NoContent();
            });
        }

        [HttpGet]
        [ActionName("bulk-get")]
        [SwaggerOperation("Retrieves all keyword entities.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keywords retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entities not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> BulkGetAsync()
        {
            return HandleRequestAsync(async () =>
            {
                var maybeKeywords = await _keywordService.GetAllAsync();
                return maybeKeywords.Any() ? Ok(maybeKeywords.ToResponse()) : NoContent();
            });
        }

        [HttpGet]
        [ActionName("get")]
        [SwaggerOperation("Retrieve keyword entity by Id.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keyword retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> GetAsync(long id)
        {
            return HandleRequestAsync(async () =>
            {
                if (id <= 0) return BadRequest();
                var maybeKeyword = await _keywordService.GetAsync(id);
                return maybeKeyword == null ? NotFound() : Ok(maybeKeyword.ToResponse());
            });
        }

        [HttpGet]
        [ActionName("search")]
        [SwaggerResponse(StatusCodes.Status200OK, "Keyword retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Technical Error.")]
        public Task<IActionResult> SearchAsync(string keywordName)
        {
            return HandleRequestAsync(async () =>
            {
                if (string.IsNullOrEmpty(keywordName)) return BadRequest();
                var maybeKeywords = await _keywordService.SearchAsync(keywordName);
                return !maybeKeywords.Any() ? NotFound() : Ok(maybeKeywords.ToResponse());
            });
        }
    }
}

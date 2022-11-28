using FluentValidation;
using SamplePoc.Contracts.Request;

namespace SamplePoc.Host.Validators
{
    public class CampaignUpdateRequestValidator : AbstractValidator<CampaignUpdateRequest>
    {
        public CampaignUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ModifiedBy).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
        }
    }
}

using FluentValidation;
using SamplePoc.Contracts.Request;

namespace SamplePoc.Host.Validators
{
    public class CampaignBulkAddRequestValidator : AbstractValidator<CampaignBulkAddRequest>
    {
        public CampaignBulkAddRequestValidator(IValidator<CampaignAddRequest> campaignValidator)
        {
            RuleForEach(x => x.Campaigns).SetValidator(campaignValidator);
        }
    }
}

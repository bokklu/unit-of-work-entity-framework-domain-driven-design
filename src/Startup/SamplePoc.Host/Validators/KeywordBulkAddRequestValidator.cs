using FluentValidation;
using SamplePoc.Contracts.Request;

namespace SamplePoc.Host.Validators
{
    public class KeywordBulkAddRequestValidator : AbstractValidator<KeywordBulkAddRequest>
    {
        public KeywordBulkAddRequestValidator(IValidator<KeywordAddRequest> keywordAddValidator)
        {
            RuleForEach(x => x.Keywords).SetValidator(keywordAddValidator);
        }
    }
}

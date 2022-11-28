using FluentValidation;
using SamplePoc.Contracts.Request;

namespace SamplePoc.Host.Validators
{
    public class KeywordAddRequestValidator : AbstractValidator<KeywordAddRequest>
    {
        public KeywordAddRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ModifiedBy).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
        }
    }
}

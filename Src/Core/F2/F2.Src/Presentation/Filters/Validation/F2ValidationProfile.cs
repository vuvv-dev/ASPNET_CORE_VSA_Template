using FluentValidation;

namespace F2.Src.Presentation.Filters.Validation;

public sealed class F2ValidationProfile : AbstractValidator<F2Request>
{
    public F2ValidationProfile()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.ListId).Must(prop => prop > 0);
    }
}

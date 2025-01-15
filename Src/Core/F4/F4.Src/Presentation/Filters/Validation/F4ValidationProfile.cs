using FluentValidation;

namespace F4.Src.Presentation.Filters.Validation;

public sealed class F4ValidationProfile : AbstractValidator<F4Request>
{
    public F4ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Email).NotEmpty().EmailAddress();
    }
}

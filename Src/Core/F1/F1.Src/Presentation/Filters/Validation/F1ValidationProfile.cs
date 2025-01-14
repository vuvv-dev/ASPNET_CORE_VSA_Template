using FluentValidation;

namespace F1.Src.Presentation.Filters.Validation;

public sealed class F1ValidationProfile : AbstractValidator<F1Request>
{
    public F1ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Email).NotEmpty().EmailAddress();

        RuleFor(prop => prop.Password).NotEmpty();

        RuleFor(prop => prop.RememberMe).NotNull();
    }
}

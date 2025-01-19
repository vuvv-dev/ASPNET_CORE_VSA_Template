using FConfig;
using FluentValidation;

namespace F1.Presentation.Filters.Validation;

public sealed class F1ValidationProfile : AbstractValidator<F1Request>
{
    public F1ValidationProfile(AspNetCoreIdentityOptions aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Email).NotEmpty().EmailAddress();

        RuleFor(prop => prop.Password)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);

        RuleFor(prop => prop.RememberMe).NotNull();
    }
}

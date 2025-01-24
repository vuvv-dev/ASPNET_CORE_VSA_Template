using FConfig;
using FluentValidation;

namespace F3.Presentation.Filters.Validation;

public sealed class F3ValidationProfile : AbstractValidator<F3Request>
{
    public F3ValidationProfile(AspNetCoreIdentityOption aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Email).NotEmpty().EmailAddress();

        RuleFor(prop => prop.Password)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);
    }
}

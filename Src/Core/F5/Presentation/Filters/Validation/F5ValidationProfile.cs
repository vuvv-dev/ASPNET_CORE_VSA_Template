using FConfig;
using FluentValidation;

namespace F5.Presentation.Filters.Validation;

public sealed class F5ValidationProfile : AbstractValidator<F5Request>
{
    public F5ValidationProfile(AspNetCoreIdentityOption aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.NewPassword)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);
    }
}

using FConfig.Src;
using FluentValidation;

namespace F5.Src.Presentation.Filters.Validation;

public sealed class F5ValidationProfile : AbstractValidator<F5Request>
{
    public F5ValidationProfile(AspNetCoreIdentityOptions aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.NewPassword)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);
    }
}

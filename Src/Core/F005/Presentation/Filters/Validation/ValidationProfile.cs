using Base.Config;
using FluentValidation;

namespace F005.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile(AspNetCoreIdentityOption aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.NewPassword)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);
    }
}

using FConfig;
using FluentValidation;

namespace F3.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile(AspNetCoreIdentityOption aspNetCoreIdentityOptions)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Email).NotEmpty().EmailAddress();

        RuleFor(prop => prop.Password)
            .NotEmpty()
            .MinimumLength(aspNetCoreIdentityOptions.Password.RequiredLength);
    }
}

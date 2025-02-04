using FCommon.Constants;
using FluentValidation;

namespace F6.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.RefreshToken)
            .NotEmpty()
            .MinimumLength(AppConstant.REFRESH_TOKEN_LENGTH);
    }
}

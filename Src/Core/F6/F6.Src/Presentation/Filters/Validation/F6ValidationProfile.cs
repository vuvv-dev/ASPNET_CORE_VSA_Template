using FCommon.Src.Constants;
using FluentValidation;

namespace F6.Src.Presentation.Filters.Validation;

public sealed class F6ValidationProfile : AbstractValidator<F6Request>
{
    public F6ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.RefreshToken)
            .NotEmpty()
            .MinimumLength(AppConstants.REFRESH_TOKEN_LENGTH);
    }
}

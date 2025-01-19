using FluentValidation;

namespace F11.Presentation.Filters.Validation;

public sealed class F11ValidationProfile : AbstractValidator<F11Request>
{
    public F11ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

using FluentValidation;

namespace F13.Presentation.Filters.Validation;

public sealed class F13ValidationProfile : AbstractValidator<F13Request>
{
    public F13ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

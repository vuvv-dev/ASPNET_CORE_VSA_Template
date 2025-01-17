using FluentValidation;

namespace F8.Src.Presentation.Filters.Validation;

public sealed class F8ValidationProfile : AbstractValidator<F8Request>
{
    public F8ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

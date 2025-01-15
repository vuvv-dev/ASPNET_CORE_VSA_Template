using FluentValidation;

namespace F5.Src.Presentation.Filters.Validation;

public sealed class F5ValidationProfile : AbstractValidator<F5Request>
{
    public F5ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

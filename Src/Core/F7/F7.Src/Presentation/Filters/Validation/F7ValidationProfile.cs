using FluentValidation;

namespace F7.Src.Presentation.Filters.Validation;

public sealed class F7ValidationProfile : AbstractValidator<F7Request>
{
    public F7ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

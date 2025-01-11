using FluentValidation;

namespace F1.Src.Presentation.Filters.Validation;

public sealed class F1ValidationProfile : AbstractValidator<F1Request>
{
    public F1ValidationProfile()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

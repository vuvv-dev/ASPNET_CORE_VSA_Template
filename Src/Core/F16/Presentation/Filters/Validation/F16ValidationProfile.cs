using FluentValidation;

namespace F16.Presentation.Filters.Validation;

public sealed class F16ValidationProfile : AbstractValidator<F16Request>
{
    public F16ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);
    }
}

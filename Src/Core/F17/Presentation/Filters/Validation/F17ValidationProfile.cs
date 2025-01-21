using FluentValidation;

namespace F17.Presentation.Filters.Validation;

public sealed class F17ValidationProfile : AbstractValidator<F17Request>
{
    public F17ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);
    }
}

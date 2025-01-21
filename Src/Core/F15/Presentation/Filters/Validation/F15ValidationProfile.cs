using FluentValidation;

namespace F15.Presentation.Filters.Validation;

public sealed class F15ValidationProfile : AbstractValidator<F15Request>
{
    public F15ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);
    }
}

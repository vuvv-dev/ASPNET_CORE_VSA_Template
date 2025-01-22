using FluentValidation;

namespace F18.Presentation.Filters.Validation;

public sealed class F18ValidationProfile : AbstractValidator<F18Request>
{
    public F18ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);
    }
}

using FluentValidation;

namespace F14.Presentation.Filters.Validation;

public sealed class F14ValidationProfile : AbstractValidator<F14Request>
{
    public F14ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListId).Must(prop => prop >= 0);

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);

        RuleFor(prop => prop.NumberOfRecord).Must(prop => prop > 0);
    }
}

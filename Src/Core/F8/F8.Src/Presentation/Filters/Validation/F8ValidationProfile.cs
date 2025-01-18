using FluentValidation;

namespace F8.Src.Presentation.Filters.Validation;

public sealed class F8ValidationProfile : AbstractValidator<F8Request>
{
    public F8ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListId).Must(prop => prop >= 0);
    }
}

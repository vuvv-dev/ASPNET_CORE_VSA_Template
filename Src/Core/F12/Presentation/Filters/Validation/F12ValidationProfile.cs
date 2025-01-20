using FluentValidation;

namespace F12.Presentation.Filters.Validation;

public sealed class F12ValidationProfile : AbstractValidator<F12Request>
{
    public F12ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);
    }
}

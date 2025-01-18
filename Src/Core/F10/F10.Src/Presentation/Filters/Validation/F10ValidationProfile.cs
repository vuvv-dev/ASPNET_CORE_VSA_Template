using FluentValidation;

namespace F10.Src.Presentation.Filters.Validation;

public sealed class F10ValidationProfile : AbstractValidator<F10Request>
{
    public F10ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListId).Must(prop => prop >= 0);

        RuleFor(prop => prop.NumberOfRecord).Must(prop => prop > 0 && prop <= 10);
    }
}

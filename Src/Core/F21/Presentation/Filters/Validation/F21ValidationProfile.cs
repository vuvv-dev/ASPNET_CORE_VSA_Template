using System;
using FluentValidation;

namespace F21.Presentation.Filters.Validation;

public sealed class F21ValidationProfile : AbstractValidator<F21Request>
{
    public F21ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);

        RuleFor(prop => prop.DueDate).Must(prop => prop >= DateTime.MinValue);
    }
}

using FA1.Entities;
using FluentValidation;

namespace F20.Presentation.Filters.Validation;

public sealed class F20ValidationProfile : AbstractValidator<F20Request>
{
    public F20ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);

        RuleFor(prop => prop.Note)
            .NotEmpty()
            .MaximumLength(TodoTaskEntity.Metadata.Properties.Note.MaxLength);
    }
}

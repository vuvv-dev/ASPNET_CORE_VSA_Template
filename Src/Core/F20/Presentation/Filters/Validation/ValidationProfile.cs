using FA1.Entities;
using FluentValidation;

namespace F20.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);

        RuleFor(prop => prop.Note)
            .NotEmpty()
            .MaximumLength(TodoTaskEntity.Metadata.Properties.Note.MaxLength);
    }
}

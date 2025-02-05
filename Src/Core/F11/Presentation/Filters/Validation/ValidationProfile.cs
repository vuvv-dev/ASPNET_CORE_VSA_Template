using FA1.Entities;
using FluentValidation;

namespace F11.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.Content)
            .NotEmpty()
            .MaximumLength(TodoTaskEntity.Metadata.Properties.Content.MaxLength);

        RuleFor(prop => prop.TodoTaskListId).Must(prop => prop >= 0);
    }
}

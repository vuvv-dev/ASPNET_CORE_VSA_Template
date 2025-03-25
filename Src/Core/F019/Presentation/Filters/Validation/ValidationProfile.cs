using Base.FX001.Entities;
using FluentValidation;

namespace F019.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskId).Must(prop => prop >= 0);

        RuleFor(prop => prop.Content)
            .NotEmpty()
            .MaximumLength(TodoTaskEntity.Metadata.Properties.Content.MaxLength);
    }
}

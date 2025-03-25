using Base.FX001.Entities;
using FluentValidation;

namespace F020.Presentation.Filters.Validation;

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

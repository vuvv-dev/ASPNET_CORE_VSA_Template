using Base.FX001.Entities;
using FluentValidation;

namespace F009.Presentation.Filters.Validation;

public sealed class ValidationProfile : AbstractValidator<Request>
{
    public ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListId).NotEmpty();

        RuleFor(prop => prop.NewName)
            .NotEmpty()
            .MaximumLength(TodoTaskListEntity.Metadata.Properties.Name.MaxLength);
    }
}

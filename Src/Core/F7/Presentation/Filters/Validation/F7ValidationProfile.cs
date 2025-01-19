using FA1.Entities;
using FluentValidation;

namespace F7.Presentation.Filters.Validation;

public sealed class F7ValidationProfile : AbstractValidator<F7Request>
{
    public F7ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListName)
            .NotEmpty()
            .MaximumLength(TodoTaskListEntity.Metadata.Properties.Name.MaxLength);
    }
}

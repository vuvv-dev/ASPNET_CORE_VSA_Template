using FA1.Src.Entities;
using FluentValidation;

namespace F7.Src.Presentation.Filters.Validation;

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

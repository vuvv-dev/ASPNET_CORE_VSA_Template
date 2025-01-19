using FA1.Entities;
using FluentValidation;

namespace F9.Presentation.Filters.Validation;

public sealed class F9ValidationProfile : AbstractValidator<F9Request>
{
    public F9ValidationProfile()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(prop => prop.TodoTaskListId).NotEmpty();

        RuleFor(prop => prop.NewName)
            .NotEmpty()
            .MaximumLength(TodoTaskListEntity.Metadata.Properties.Name.MaxLength);
    }
}

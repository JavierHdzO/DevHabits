using FluentValidation;

namespace DevHabit.Api.DTOs.Tags;

public sealed class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
{
    public CreateTagDtoValidator()
    {
        RuleFor(h => h.Name).NotEmpty().MinimumLength(3);

        RuleFor(h => h.Description).MaximumLength(50);

    }
}

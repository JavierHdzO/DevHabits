using DevHabit.Api.Entities;
using FluentValidation;

namespace DevHabit.Api.DTOs.Habits;

public sealed class CreateHabitDtoValidator : AbstractValidator<CreateHabitDto> 
{
    private static readonly string[] AllowedUnits = 
    [
        "minutes", "hours", "steps", "km", "cal",
        "pages", "books", "tasks", "sessions"
    ];

    private static readonly string[] AllowedUnitsForBinaryHabits = ["sessions","tasks"];

    public CreateHabitDtoValidator()
    {
        RuleFor(h => h.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Habit name must be between 3 and 100 characters");

        RuleFor(h => h.Description)
            .MaximumLength(500)
            .When(h => h.Description is not null)
            .WithMessage("Description cannot exceed 500 characters");

        RuleFor(h => h.Type)
            .IsInEnum()
            .WithMessage("Habit type is invalid");

        //Frequency validation
        RuleFor(h => h.Frequency.Type)
            .IsInEnum()
            .WithMessage("Frequency type is invalid");

        RuleFor(h => h.Frequency.TimesPerPeriod)
            .GreaterThan(0)
            .WithMessage("Frequency times per period must be greater than 0");

        //Target validation
        RuleFor(h => h.Target.Value)
            .GreaterThan(0)
            .WithMessage("Target value must be greater than 0");

        RuleFor(h => h.Target.Unit)
            .NotEmpty()
            .Must(unit => AllowedUnits.Contains(unit.ToLowerInvariant()))
            .WithMessage($"Target unit must be one of the following: {string.Join(", ", AllowedUnits)}");

        //End date validation
        RuleFor(h => h.DateEnd)
            .Must(date => date is null || date.Value > DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("End date must be in the future");

        //Milestone validation
        When(x => x.Milestone is not null, () => 
        {
            RuleFor( h => h.Milestone!.Target)
                .GreaterThan(0)
                .WithMessage("Milestone target must be greater than 0");

        });

        RuleFor( h => h.Target.Unit)
            .Must(( dto, unit ) => IsTargetUnitCompatibleWithType(dto.Type, unit))
            .WithMessage(dto => $"Target unit is not compatible with habit type");

    }

    private static bool IsTargetUnitCompatibleWithType(HabitType type, string unit) 
    {
        string normalizedUnit = unit.ToLowerInvariant();

        return type switch
        {
            // Binary habits should only use couunt-based units.
            HabitType.Binary => AllowedUnitsForBinaryHabits.Contains(normalizedUnit),
            // Measurable habits can use any of the allowed units.
            HabitType.Measurable => AllowedUnits.Contains(normalizedUnit),
            _ => false // None is not valid
        };

    }

}

using System.Security.Cryptography.X509Certificates;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static  class HabitMappings
{
    public static HabitDto ToDto(this Habit habit)
    {
        return new HabitDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            Type = habit.Type,
            Frequency = new FrequencyDto
            {
                TimesPerPeriod = habit.Frequency.TimesPerPeriod,
                Type = habit.Frequency.Type
            },
            Target = new TargetDto
            {
                Unit = habit.Target.Unit,
                Value = habit.Target.Value
            },
            Status = habit.Status,
            IsArchived = habit.IsArchived,
            DateEnd = habit.DateEnd,
            Milestone = habit.Milestone == null ? null : new MilestoneDto
            {
                Current = habit.Milestone.Current,
                Target = habit.Milestone.Current
            },
            CreatedAtUtc = habit.CreatedAtUtc,
            UpdatedAtUtc = habit.UpdatedAtUtc,
            LastCompletedAtUtc = habit.LastCompletedAtUtc
        };

    }
    public static Habit ToEntity(this CreateHabitDto dto) 
    {

        Habit habit = new ()
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type,
            Frequency = new Frequency
            {
                TimesPerPeriod = dto.Frequency.TimesPerPeriod,
                Type = dto.Frequency.Type
            },
            Target = new Target
            {
                Unit = dto.Target.Unit,
                Value = dto.Target.Value
            },
            DateEnd = dto.DateEnd,
            Milestone = dto.Milestone is null ? null : new Milestone
            {
                Current = 0,
                Target = 0
            },
            Status = HabitStatus.Ongoing,
            IsArchived = false,
            CreatedAtUtc = DateTime.UtcNow,

        };

        return habit;

    }

    public static void  UpdateFromDto(this Habit habit, UpdateHabitDto habitDto) 
    {
        //Update basic properties
        habit.Name = habitDto.Name;
        habit.Description = habitDto.Description;
        habit.Type = habitDto.Type;
        habit.DateEnd = habitDto.DateEnd;

        //Update frequency (assuming it's inmutable, create new instance)
        habit.Frequency = new Frequency
        {
            TimesPerPeriod = habitDto.Frequency.TimesPerPeriod,
            Type = habitDto.Frequency.Type
        };

        //Update target
        habit.Target = new Target
        {
            Unit = habitDto.Target.Unit,
            Value = habitDto.Target.Value
        };

        //Update milestone if provided
        if (habitDto.Milestone is not null)
        {
            habit.Milestone ??= new Milestone(); // Create if null
            habit.Milestone.Target = habitDto.Milestone.Target;
            //Note: We don't update Mullestone.Current from DTO to preserve the progress
        }


        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}

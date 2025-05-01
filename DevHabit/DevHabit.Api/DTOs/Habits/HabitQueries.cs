using System.Linq.Expressions;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitQueries
{

    public static Expression<Func<Habit, HabitDto>> ProjectToDto()
    {
        return h => new HabitDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            Type = h.Type,
            Frequency = new FrequencyDto
            {
                TimesPerPeriod = h.Frequency.TimesPerPeriod,
                Type = h.Frequency.Type
            },
            Target = new TargetDto
            {
                Unit = h.Target.Unit,
                Value = h.Target.Value
            },
            Status = h.Status,
            IsArchived = h.IsArchived,
            DateEnd = h.DateEnd,
            Milestone = h.Milestone == null ? null : new MilestoneDto
            {
                Current = h.Milestone.Current,
                Target = h.Milestone.Target
            },
            CreatedAtUtc = h.CreatedAtUtc,
            UpdatedAtUtc = h.UpdatedAtUtc,
            LastCompletedAtUtc = h.LastCompletedAtUtc
        };

    }

    public static Expression<Func<Habit, HabitWithTagsDto>> ProjectToHabitWithTagsDto()
    {
        return h => new HabitWithTagsDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            Type = h.Type,
            Frequency = new FrequencyDto
            {
                TimesPerPeriod = h.Frequency.TimesPerPeriod,
                Type = h.Frequency.Type
            },
            Target = new TargetDto
            {
                Unit = h.Target.Unit,
                Value = h.Target.Value
            },
            Status = h.Status,
            IsArchived = h.IsArchived,
            DateEnd = h.DateEnd,
            Milestone = h.Milestone == null ? null : new MilestoneDto
            {
                Current = h.Milestone.Current,
                Target = h.Milestone.Target
            },
            CreatedAtUtc = h.CreatedAtUtc,
            UpdatedAtUtc = h.UpdatedAtUtc,
            LastCompletedAtUtc = h.LastCompletedAtUtc,
            Tags = h.Tags.Select(t => t.Name).ToArray()
        };

    }

}

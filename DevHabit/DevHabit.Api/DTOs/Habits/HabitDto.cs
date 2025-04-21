﻿using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

public sealed record HabitCollectionDto 
{
    public List<HabitDto> Data { get; init; }
}

public sealed record HabitDto
{
    public required string Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required string? Description { get; init; }
    public required HabitType Type { get; init; }
    public required FrequencyDto Frequency { get; init; }
    public required TargetDto Target { get; init; }
    public required HabitStatus Status { get; init; }
    public required bool IsArchived { get; init; }
    public DateOnly? DateEnd { get; init; }
    public MilestoneDto? Milestone { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public DateTime? LastCompletedAtUtc { get; init; }


}

public sealed class FrequencyDto
{
    public required  FrequencyType Type { get; init; }
    public required int TimesPerPeriod { get; init; }

}


public sealed class TargetDto
{
    public required int Value { get; init; }
    public required string Unit { get; init; }
}


public sealed class MilestoneDto
{
    public required int Target { get; init; }
    public required int Current { get; init; }
}


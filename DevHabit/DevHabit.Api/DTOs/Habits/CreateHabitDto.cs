﻿using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

public sealed record CreateHabitDto
{
    public required string Name { get; init; } = string.Empty;
    public required string? Description { get; init; }
    public required HabitType Type { get; init; }
    public required FrequencyDto Frequency { get; init; }
    public required TargetDto Target { get; init; }
    public DateOnly? DateEnd { get; init; }
    public MilestoneDto? Milestone { get; init; }
}

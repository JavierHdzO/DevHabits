﻿using DevHabit.Api.Database;
using DevHabit.Api.Entities;
using DevHabit.Api.DTOs.HabitTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

[ApiController]
[Route("habits/{habitId}/tags")]
public sealed class HabitTagsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult> UpsertHabitTags(string habitId, UpsertHabitTagsDto upsertHabitTagsDto) 
    {

        Habit? habit = await dbContext.Habits
            .Include(h => h.HabitTags)
            .FirstOrDefaultAsync( h => h.Id == habitId);

        if (habit is null)
        {
            return NotFound();
        }

        var currentTagIds = habit.HabitTags.Select(h => h.TagId).ToHashSet();

        if (currentTagIds.SetEquals(upsertHabitTagsDto.TagIds))
        {
            return NoContent();
        }


        List<string> existingTagIds = await dbContext.Tags
            .Where(t => upsertHabitTagsDto.TagIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();

        if (existingTagIds.Count != upsertHabitTagsDto.TagIds.Count)
        {
            return BadRequest("One or more Tag IDs is invalid");
        }

        habit.HabitTags.RemoveAll( h => !upsertHabitTagsDto.TagIds.Contains(h.TagId));

        string[] tagIdsToAdd = upsertHabitTagsDto.TagIds.Except(currentTagIds).ToArray();

        habit.HabitTags.AddRange(
            tagIdsToAdd.Select(tagId => new HabitTag
            {
                HabitId = habit.Id,
                TagId = tagId,
                CreatedAtUtc = DateTime.UtcNow
            })
        );

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{tagId}")]
    public async Task<ActionResult> DeleteHabitTag(string habitId, string tagId)
    {
        HabitTag? habitTag = await dbContext.HabitTags
            .SingleOrDefaultAsync(h => h.HabitId == habitId && h.TagId == tagId);


        if (habitTag is null)
        {
            return NotFound();  
        }

        dbContext.HabitTags.Remove(habitTag);

        await dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}

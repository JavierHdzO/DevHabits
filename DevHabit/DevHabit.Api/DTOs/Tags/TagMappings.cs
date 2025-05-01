using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Tags;

internal static class TagMappings
{
    public static TagDto ToDto(this Tag tag)
    {
        return new()
        {
            Id = tag.Id,
            Name = tag.Name,
            Description = tag.Description,
            CreatedAtUtc = tag.CreatedAtUtc,
            UpdatedAtUtc = tag.UpdatedAtUtc,
        };
    }

    public static Tag ToEntity(this CreateTagDto dto)
    {
        return new()
        {
            Id = $"t_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Description = dto.Description,
            CreatedAtUtc = DateTime.UtcNow,
        };
    
    }

    public static void UpdateFromDto(this Tag tag, UpdateTagDto updateTagDto) 
    {
        tag.Name = updateTagDto.Name;
        tag.Description = updateTagDto.Description;
        tag.UpdatedAtUtc = DateTime.UtcNow;
    }

}

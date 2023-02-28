using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;
using Guid = System.Guid;

namespace ClassScheduler.Data.Mappers;

public class LearningEventMapper : IEntityDtoMapper<LearningEvent, LearningEventDto>
{
    public LearningEvent MapDtoToEntity(LearningEventDto dto)
    {
        var args = new LearningEventArgs()
        {
            Description = dto.Description,
            EndTime = dto.EndTime,
            Id = dto.Id,
            StartTime = dto.StartTime,
            StudentCapacity = dto.StudentCapacity,
            Title = dto.Title,
            TotalHours = dto.TotalHours,
        };

        return new LearningEvent(args);
    }

    public LearningEventDto MapEntityToDto(LearningEvent entity)
    {
        var studentIds = entity.Students.Select(student => student.Id.ToString()).ToList();

        return new LearningEventDto()
        {
            Description = entity.Description,
            EndTime = entity.EndTime,
            Id = entity.Id,
            StartTime = entity.StartTime,
            StudentCapacity = entity.StudentCapacity,
            StudentIds = studentIds,
            Title = entity.Title,
            TotalHours = entity.TotalHours
        };
    }
}
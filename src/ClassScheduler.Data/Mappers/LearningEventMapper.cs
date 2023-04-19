using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;
using Guid = System.Guid;

namespace ClassScheduler.Data.Mappers;

public static class LearningEventMapper
{
    public static LearningEvent MapDtoToEntity(LearningEventDto dto, List<Student> students)
    {
        var args = new LearningEventArgs()
        {
            Description = dto.Description,
            EndTime = dto.EndTime,
            Id = dto.Id,
            StartTime = dto.StartTime,
            MaxStudents = dto.StudentCapacity,
            Title = dto.Title,
            TotalHours = dto.TotalHours,
            Students = students.ToList()
        };

        return new LearningEvent(args);
    }

    public static LearningEventDto MapEntityToDto(LearningEvent entity)
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
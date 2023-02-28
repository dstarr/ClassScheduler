using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Mappers;

public class StudentMapper : IEntityDtoMapper<Student, StudentDto>
{
    public Student MapDtoToEntity(StudentDto dto)
    {
        return new Student(dto.Id, dto.FirstName, dto.LastName, dto.Email);
    }

    public StudentDto MapEntityToDto(Student entity)
    {
        return new StudentDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };
    }
}
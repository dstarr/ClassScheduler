using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Mappers;

public static class StudentMapper
{
    public static Student MapDtoToEntity(StudentDto dto)
    {
        if (dto == null) throw new ArgumentNullException();

        return new Student(dto.Id, dto.FirstName, dto.LastName, dto.Email);
    }

    public static StudentDto MapEntityToDto(Student entity)
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
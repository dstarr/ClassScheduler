using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _dbContext;

    private readonly IEntityDtoMapper<Student, StudentDto> _mapper = new StudentMapper();

    public StudentRepository(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreatedAsync();
    }
    
    public async Task<Student> AddAsync(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        var studentDto = await _dbContext.Students.AddAsync(dto);
        await _dbContext.SaveChangesAsync();
        
        var student = _mapper.MapDtoToEntity(studentDto.Entity);

        return student;
    }

    public async Task<Student> UpdateAsync(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        var studentDto = _dbContext.Students.Update(dto).Entity;
        await _dbContext.SaveChangesAsync();

        var student = _mapper.MapDtoToEntity(studentDto);

        return student;

    }

    public async Task<Student> RemoveAsync(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        var studentDto = _dbContext.Students.Remove(dto).Entity;
        await _dbContext.SaveChangesAsync();

        var student = _mapper.MapDtoToEntity(studentDto);

        return student;
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.Students.FindAsync(id);

        return _mapper.MapDtoToEntity(dto);
    }

    public IList<Student> GetAll()
    {
        var students = new List<Student>();

        var dtos = _dbContext.Students;

        foreach (var dto in dtos)
        {
            var student = _mapper.MapDtoToEntity(dto);
            students.Add(student);
        }

        return students;
    }
}
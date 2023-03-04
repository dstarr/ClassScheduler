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
        var toDto = _mapper.MapEntityToDto(entity);

        var fromDto = (await _dbContext.Students.AddAsync(toDto)).Entity;
        await _dbContext.SaveChangesAsync();
        
        var student = _mapper.MapDtoToEntity(fromDto);

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
        var toDto = _mapper.MapEntityToDto(entity);

        var fromDto = _dbContext.Students.Remove(toDto).Entity;
        await _dbContext.SaveChangesAsync();

        var student = _mapper.MapDtoToEntity(fromDto);

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
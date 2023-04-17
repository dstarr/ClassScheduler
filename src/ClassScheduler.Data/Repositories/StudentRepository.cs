using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _dbContext;

    public StudentRepository(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task AddAsync(Student entity)
    {
        var toDto = StudentMapper.MapEntityToDto(entity);

        await _dbContext.Students.AddAsync(toDto);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        var dto = _dbContext.Students.FirstOrDefault(u => u.Id == student.Id);

        if (dto == null) return;

        dto.FirstName = student.FirstName;
        dto.LastName = student.LastName;
        dto.Email = student.Email;
        dto.PartitionKey = "/id";

        _dbContext.Students.Update(dto);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dto = _dbContext.Students.FirstOrDefault(u => u.Id == id);
    
        if (dto == null) return;


        _dbContext.Set<StudentDto>().Remove(dto);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);

        if (dto == null) return null!;

        return StudentMapper.MapDtoToEntity(dto);
    }

    public async Task<IList<Student>> GetAllAsync()
    {
        var students = new List<Student>();

        var dtos = await _dbContext.Students.ToListAsync();

        foreach (var dto in dtos)
        {
            var student = StudentMapper.MapDtoToEntity(dto);
            students.Add(student);
        }

        return students;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
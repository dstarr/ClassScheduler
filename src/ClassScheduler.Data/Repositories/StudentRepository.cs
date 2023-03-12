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

    public void Update(Student student)
    {
        var dto = _dbContext.Students.FirstOrDefault(u => u.Id == student.Id);

        if (dto == null) return;

        dto.FirstName = student.FirstName;
        dto.LastName = student.LastName;
        dto.Email = student.Email;
        
        _dbContext.SaveChanges();
    }

    public async Task RemoveAsync(Student entity)
    {
        var dto = _dbContext.Students.FirstOrDefault(u => u.Id == entity.Id);
        if (dto == null) return;

        _dbContext.Students.Remove(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);

        if (dto == null) return null!;

        return _mapper.MapDtoToEntity(dto);
    }

    public async Task<IList<Student>> GetAllAsync()
    {
        var students = new List<Student>();

        var dtos = await _dbContext.Students.ToListAsync();

        foreach (var dto in dtos)
        {
            var student = _mapper.MapDtoToEntity(dto);
            students.Add(student);
        }

        return students;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _dbContext;
    private readonly DbSet<StudentDto> _dbSet;

    private readonly IEntityDtoMapper<Student, StudentDto> _mapper = new StudentMapper();

    public StudentRepository(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<StudentDto>();
    }
    
    public async Task AddAsync(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        await _dbSet.AddAsync(dto);
    }

    public void Update(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        _dbSet.Update(dto);
    }

    public void Remove(Student entity)
    {
        var dto = _mapper.MapEntityToDto(entity);

        _dbSet.Remove(dto);
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        var dto = await _dbSet.FindAsync(id);

        return _mapper.MapDtoToEntity(dto);
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
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

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
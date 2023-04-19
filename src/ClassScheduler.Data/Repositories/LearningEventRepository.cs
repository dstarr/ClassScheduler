using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static ClassScheduler.Data.Mappers.LearningEventMapper;

namespace ClassScheduler.Data.Repositories;

public class LearningEventRepository : ILearningEventRepository
{
    private readonly AppDbContext _dbContext;

    public LearningEventRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(LearningEvent entity)
    {
        var toDto = MapEntityToDto(entity);

        var studentDtos = new List<StudentDto>();
        foreach (var student in entity.Students)
        {
            var studentDto = await _dbContext.Students.FirstOrDefaultAsync();
            
            if (studentDto == null)
            {
                studentDto = StudentMapper.MapEntityToDto(student);
            }

            studentDtos.Add(studentDto);
        }

        await _dbContext.Set<LearningEventDto>().AddAsync(toDto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(LearningEvent entity)
    {
        var dto = _dbContext.LearningEvents.FirstOrDefault(u => u.Id == entity.Id);

        if (dto == null) return;

        var studentIds = new List<string>();
        foreach (var student in entity.Students)
        {
            studentIds.Add(student.Id.ToString());            
        }

        dto.EndTime = entity.EndTime;
        dto.StartTime = entity.StartTime;
        dto.Id = entity.Id;
        dto.Description = entity.Description;
        dto.StudentCapacity = entity.StudentCapacity;
        dto.TotalHours = entity.TotalHours;
        dto.StudentIds = studentIds;
        dto.Title = entity.Title;

        // _dbContext.Set<LearningEventDto>().Update(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dto = _dbContext.LearningEvents.FirstOrDefault(u => u.Id == id);    

        if (dto == null) return;

        _dbContext.Set<LearningEventDto>().Remove(dto);

        await _dbContext.SaveChangesAsync();
    }


    public async Task<LearningEvent> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.LearningEvents
            .SingleOrDefaultAsync(s => s.Id == id);
        
        if (dto == null) return null!;
        
        var studentDtos = await _dbContext.Students.ToListAsync();

        var students = studentDtos.Select(StudentMapper.MapDtoToEntity).ToList();
        
        return LearningEventMapper.MapDtoToEntity(dto, students);
    }


    public async Task<IList<LearningEvent>> GetAllAsync()
    {
        var dtos = await _dbContext.LearningEvents.ToListAsync();
        
        var entities = new List<LearningEvent>();
        
        foreach (var dto in dtos)
        {
            var studentDtos = _dbContext.Students.ToList();

            var students = studentDtos.Select(StudentMapper.MapDtoToEntity).ToList();
        
            var entity= dtos.Select(learningEventDto => MapDtoToEntity(learningEventDto, students)).FirstOrDefault();

            if (entity != null)
            {
                entities.Add(entity);
            }
        }
        
        return entities;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
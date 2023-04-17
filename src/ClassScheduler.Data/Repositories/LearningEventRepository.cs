using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ClassScheduler.Data.Repositories;

public class LearningEventRepository : ILearningEventRepository
{
    private readonly LearningEventDbContext _learningEventDbContext;
    private readonly StudentDbContext _studentDbContext;

    public LearningEventRepository(LearningEventDbContext learningEventDbContext, StudentDbContext studentDbContext)
    {
        _learningEventDbContext = learningEventDbContext;
        _studentDbContext = studentDbContext;
    }

    public async Task AddAsync(LearningEvent entity)
    {
        var toDto = LearningEventMapper.MapEntityToDto(entity);

        await _learningEventDbContext.Set<LearningEventDto>().AddAsync(toDto);

        await _learningEventDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(LearningEvent entity)
    {
        var dto = _learningEventDbContext.LearningEvents.FirstOrDefault(u => u.Id == entity.Id);

        if (dto == null) return;

        dto.EndTime = entity.EndTime;
        dto.StartTime = entity.StartTime;
        dto.Id = entity.Id;
        dto.Description = entity.Description;
        dto.StudentCapacity = entity.StudentCapacity;
        dto.TotalHours = entity.TotalHours;
        dto.StudentIds = dto.StudentIds;
        dto.Title = entity.Title;

        // _learningEventDbContext.Set<LearningEventDto>().Update(dto);
        await _learningEventDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dto = _learningEventDbContext.LearningEvents.FirstOrDefault(u => u.Id == id);    

        if (dto == null) return;

        _learningEventDbContext.Set<LearningEventDto>().Remove(dto);

        await _learningEventDbContext.SaveChangesAsync();
    }


    public async Task<LearningEvent> GetByIdAsync(Guid id)
    {
        var dto = await _learningEventDbContext.LearningEvents
            .SingleOrDefaultAsync(s => s.Id == id);

        if (dto == null) return null!;

        IList<Student> students = GetStudentsList(dto);

        return LearningEventMapper.MapDtoToEntity(dto, students);
    }


    public async Task<IList<LearningEvent>> GetAllAsync()
    {
        var dtos = await _learningEventDbContext.LearningEvents.ToListAsync();

        List<LearningEvent> entities = new List<LearningEvent>();

        foreach (var dto in dtos)
        {
            IList<Student> students = GetStudentsList(dto);
            var entity= dtos.Select(learningEventDto => LearningEventMapper.MapDtoToEntity(learningEventDto, students)).FirstOrDefault();

            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        return entities;
    }

    public async ValueTask DisposeAsync()
    {
        await _learningEventDbContext.DisposeAsync();
    }
    private List<Student> GetStudentsList(LearningEventDto dto)
    {
        return dto.StudentIds
            .Select(studentId => _studentDbContext.Students
                .FirstOrDefault(s => s.Id == Guid.Parse(studentId)))
            .Select(studentDto => StudentMapper.MapDtoToEntity(studentDto))
            .ToList();
    }


}
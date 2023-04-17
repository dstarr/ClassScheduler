using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Repositories;

public class LearningEventRepository : ILearningEventRepository
{
    private readonly LearningEventDbContext _dbContext;

    public LearningEventRepository(LearningEventDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(LearningEvent entity)
    {
        var toDto = LearningEventMapper.MapEntityToDto(entity);

        await _dbContext.Set<LearningEventDto>().AddAsync(toDto);

        await _dbContext.SaveChangesAsync();
    }

    public void Update(LearningEvent entity)
    {
        var dto = _dbContext.LearningEvents.FirstOrDefault(u => u.Id == entity.Id);

        if (dto == null) return;

        dto.EndTime = entity.EndTime;
        dto.StartTime = entity.StartTime;
        dto.Id = entity.Id;
        dto.Description = entity.Description;
        dto.StudentCapacity = entity.StudentCapacity;
        dto.TotalHours = entity.TotalHours;
        dto.StudentIds = dto.StudentIds;
        dto.Title = entity.Title;

        _dbContext.Set<LearningEventDto>().Update(dto);
        _dbContext.SaveChangesAsync();
    }

    public void Remove(Guid id)
    {
        var dto = _dbContext.LearningEvents.FirstOrDefault(u => u.Id == id);    

        if (dto == null) return;

        _dbContext.Set<LearningEventDto>().Remove(dto);

        _dbContext.SaveChangesAsync();
    }


    public async Task<LearningEvent> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.LearningEvents.FirstOrDefaultAsync(s => s.Id == id);

        if (dto == null) return null!;

        return LearningEventMapper.MapDtoToEntity(dto);
    }
     

    public async Task<IList<LearningEvent>> GetAllAsync()
    {
        var dtos = await _dbContext.LearningEvents.ToListAsync();

        var entities = dtos.Select(dto => LearningEventMapper.MapDtoToEntity(dto)).ToList();

        return entities;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

}
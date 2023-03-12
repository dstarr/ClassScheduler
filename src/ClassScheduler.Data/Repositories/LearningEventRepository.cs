using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Mappers;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Repositories;

public class LearningEventRepository : ILearningEventRepository
{
    private readonly LearningEventDbContext _dbContext;

    public LearningEventRepository(LearningEventDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LearningEvent> AddAsync(LearningEvent entity)
    {
        var toDto = LearningEventMapper.MapEntityToDto(entity);

        var fromDto = (await _dbContext.LearningEvents.AddAsync(toDto)).Entity;

        var learningEvent = LearningEventMapper.MapDtoToEntity(fromDto);

        return learningEvent;
    }

    public void Update(LearningEvent entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(LearningEvent entity)
    {
        throw new NotImplementedException();
    }


    public Task<LearningEvent> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Student>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }


    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

}
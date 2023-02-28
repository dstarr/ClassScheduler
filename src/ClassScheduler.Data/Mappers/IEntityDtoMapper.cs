namespace ClassScheduler.Data.Mappers;

public interface IEntityDtoMapper<TEntity, TDto>
{
    TEntity MapDtoToEntity(TDto dto);
    TDto MapEntityToDto(TEntity entity);
}
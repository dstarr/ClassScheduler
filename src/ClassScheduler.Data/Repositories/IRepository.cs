using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Repositories;

public interface IRepository<T> : IAsyncDisposable where T : class
{
    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(Guid id);

    Task<T> GetByIdAsync(Guid id);
    
    Task<IList<T>> GetAllAsync();
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<Student> AddAsync(T entity);
    Task<Student> UpdateAsync(T entity);
    Task<Student> RemoveAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    IList<Student> GetAll();
}
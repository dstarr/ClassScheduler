﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;

namespace ClassScheduler.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveChangesAsync();
}
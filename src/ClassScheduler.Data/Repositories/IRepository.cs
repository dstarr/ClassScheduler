using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;

namespace ClassScheduler.Data.Repositories;

public interface IRepository<T> where T : class
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public T GetById(Guid id);
    public IEnumerable<T> GetAll();
}
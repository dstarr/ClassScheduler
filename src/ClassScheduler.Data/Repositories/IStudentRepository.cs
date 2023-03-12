using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Data.Repositories;

public interface IStudentRepository : IRepository<Student>, IAsyncDisposable
{
    
}

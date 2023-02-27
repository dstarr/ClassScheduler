using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data;

public class LearningEventDbContext : DbContext
{
    public DbSet<LearningEventDto> LearningEvents { get; set; } = null!;
    
    public LearningEventDbContext(DbContextOptions<LearningEventDbContext> options): base(options)
    {}
}

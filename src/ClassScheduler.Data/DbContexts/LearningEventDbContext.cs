using ClassScheduler.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.DbContexts;

public class LearningEventDbContext : DbContext
{
    public DbSet<LearningEventDto> LearningEvents { get; set; } = null!;

    public LearningEventDbContext(DbContextOptions<LearningEventDbContext> options) : base(options)
    { }
}

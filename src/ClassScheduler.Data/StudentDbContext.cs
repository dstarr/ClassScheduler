using ClassScheduler.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data;

public class StudentDbContext : DbContext
{
    public DbSet<StudentDto> Students { get; set; } = null!;

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        
    }
}
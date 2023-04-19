using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<LearningEventDto> LearningEvents { get; set; } = null!;
    public DbSet<StudentDto> Students { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningEventDto>()
            .ToContainer("LearningEvents")
            .HasKey(x => x.Id);

        modelBuilder.Entity<StudentDto>()
            .ToContainer("People")
            .HasKey(x => x.Id);
    }
}

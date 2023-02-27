using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data;

public class LearningEventDbContext : DbContext
{
    private const string DatabaseName = "ClassScheduler";
    private const string ConnectionString = "AccountEndpoint=https://class-scheduler-db.documents.azure.com:443/;AccountKey=cuDdgi38yi4abLAE8d9K1Wgi7rfNsGtOwSSjdaCxQDWKZTTLqKrwsjZnESYSiLq5txHf5yGIpIScACDbMc9V6Q==";
    
    public DbSet<LearningEvent> LearningEvents { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(ConnectionString, DatabaseName);
    }


}

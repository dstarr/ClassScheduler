using System.Configuration;
using ClassScheduler.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassScheduler.Data.Test.Integration;

public class DbTestBase
{
    internal static string ConnectionString = null!;
    internal static string DatabaseName = null!;

    public DbTestBase()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = configurationBuilder.Build();

        ConnectionString = configuration.GetSection("ConnectionString").Value ?? throw new InvalidOperationException();
        DatabaseName = configuration.GetSection("DatabaseName").Value ?? throw new InvalidOperationException();


    }

}
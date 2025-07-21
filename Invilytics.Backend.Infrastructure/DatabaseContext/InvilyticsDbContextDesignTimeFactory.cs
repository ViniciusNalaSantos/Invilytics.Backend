using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Infrastructure.DatabaseContext;
internal class InvilyticsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<InvilyticsDbContext>
{
    public InvilyticsDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var builder = new DbContextOptionsBuilder<InvilyticsDbContext>();
        var connectionString = configuration.GetConnectionString("InvilyticsDatabase");
        builder.UseSqlServer(connectionString);

        return new InvilyticsDbContext(builder.Options);
    }
}

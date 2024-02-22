using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dal.Concrete.Contexts
{
    public class PDbContextFactoryCommand : IDesignTimeDbContextFactory<PDbContextCommand>
    {
        string EndPointAssemblyName = "EndPointApi";

        PDbContextCommand IDesignTimeDbContextFactory<PDbContextCommand>.CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, EndPointAssemblyName);
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", false).Build();
            string connectionString = configuration.GetConnectionString("TestSqlServerCommand");
            var builder = new DbContextOptionsBuilder<PDbContextCommand>();
            builder.UseSqlServer(connectionString);
            return new PDbContextCommand(builder.Options);
        }
    }


    public class PDbContextFactoryQuery : IDesignTimeDbContextFactory<PDbContextQuery>
    {
        string EndPointAssemblyName = "EndPointApi";

        PDbContextQuery IDesignTimeDbContextFactory<PDbContextQuery>.CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, EndPointAssemblyName);
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", false).Build();
            string connectionString = configuration.GetConnectionString("TestSqlServerQuery");
            var builder = new DbContextOptionsBuilder<PDbContextQuery>();
            builder.UseSqlServer(connectionString);
            return new PDbContextQuery(builder.Options);
        }
    }






}

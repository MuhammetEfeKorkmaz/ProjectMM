using Entities.DbModels.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;



namespace Dal.Concrete
{
    public class PDbContextFactory : IDesignTimeDbContextFactory<PDbContext>
    {
        string EndPointAssemblyName = "EndPointApi";

        PDbContext IDesignTimeDbContextFactory<PDbContext>.CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, EndPointAssemblyName);
            Console.WriteLine("***********************");
            Console.WriteLine(path);
            Console.WriteLine("*************************");
            IConfigurationRoot configuration = new ConfigurationBuilder() 
                                             .SetBasePath(path)
                                             .AddJsonFile("appsettings.json", false).Build();

            string connectionString = configuration.GetConnectionString("TestSqlServer");


            var builder = new DbContextOptionsBuilder<PDbContext>();
            builder.UseSqlServer(connectionString);
            return new PDbContext(builder.Options);
        }
    }






    public class PDbContext : DbContext
    {
        void MyConfiguration()
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
            //ChangeTracker.AutoDetectChangesEnabled = false; 
            // ChangeTracker.CascadeChanges();


        }


        public PDbContext(DbContextOptions<PDbContext> options) : base(options)
        {
            MyConfiguration();
        }
        public PDbContext()
        {
            MyConfiguration();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Kullanılacak Veri Tabanı Ayarları yapılmamış.");
            }

        }



        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<OperationClaims> OperationClaims { get; set; } 



    }
}

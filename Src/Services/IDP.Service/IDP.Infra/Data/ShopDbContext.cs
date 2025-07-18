using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IDP.Infra.Data
{
    public class ShopDbContext:DbContext
    {

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }


        public DbSet<User> User { get; set; }

    }

    public class ShopDbContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
    {
        public ShopDbContext CreateDbContext(string[] args)
        {
            // بارگذاری appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // ایجاد DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
            var connectionString = configuration.GetConnectionString("CommandDBConnection");
            optionsBuilder.UseSqlServer(connectionString); // یا UseNpgsql برای PostgreSQL

            return new ShopDbContext(optionsBuilder.Options);
        }
    }
}

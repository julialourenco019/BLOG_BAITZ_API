using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BAITZ_BLOG_API.Context
{
    public class ApplicationDataContextFactory : IDesignTimeDbContextFactory<ApplicationDataContext>
    {
        public ApplicationDataContext CreateDbContext(string[] args)
        {
        
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();

            
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            
            optionsBuilder.UseSqlite(connectionString);

            
            return new ApplicationDataContext(optionsBuilder.Options);
        }
    }
}

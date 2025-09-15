using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FranchiseRepository
{
    /// <summary>
    /// Factory for creating instances of <see cref="FranchisDbContext"/> at design time.
    /// </summary>
    public class FranchisDbContextFactory : IDesignTimeDbContextFactory<FranchisDbContext>
    {
        /// <summary>
        /// Creates a new instance of <see cref="FranchisDbContext"/> using configuration from appsettings.json.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public FranchisDbContext CreateDbContext(string[] args)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FranchisDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new FranchisDbContext(optionsBuilder.Options);
        }
    }
}

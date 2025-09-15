using FranchiseRepository.IRepos;
using FranchiseRepository.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace FranchiseRepository
{
    /// <summary>
    /// Extension methods for registering repository services in the dependency injection container.
    /// </summary>
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Registers repository services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddDbContext<FranchisDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFranchiseRepository, FranchisRepository>();

            return services;
        }
    }
}

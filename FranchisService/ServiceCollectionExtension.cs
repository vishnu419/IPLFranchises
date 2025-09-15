using Microsoft.Extensions.DependencyInjection;
using FranchisService.IService;
using FranchisService.Service;

namespace FranchisService
{
    /// <summary>
    /// Extension methods for IServiceCollection to add domain services.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds domain services to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IFranchiseService, FranchisService.Service.FranchisService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            
            return services;
        }
    }
}

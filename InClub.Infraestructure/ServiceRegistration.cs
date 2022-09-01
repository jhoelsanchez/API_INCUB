using InClub.Application.Interfaces;
using InClub.Infraestructure.Context;
using InClub.Infraestructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API_INCUB
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<RepositoryContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}

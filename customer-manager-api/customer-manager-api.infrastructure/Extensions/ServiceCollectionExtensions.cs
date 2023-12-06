using customer_manager_api.application.Services;
using customer_manager_api.domain.Repositories;
using customer_manager_api.infrastructure.Repositories;
using customer_manager_api.infrastructure.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using static customer_management.Requests.CreateCustomerRequest;

namespace customer_manager_api.infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersService, CustomersService>();

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {           
            services.AddValidatorsFromAssembly(typeof(CreateCustomerRequestValidator).Assembly);

            return services;
        }
    }
}

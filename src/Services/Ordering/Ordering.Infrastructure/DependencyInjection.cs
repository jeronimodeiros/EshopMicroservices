using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services here
        // Example: services.AddScoped<IOrderRepository, OrderRepository>();
        // services.AddDbContext<OrderingContext>(options => 
        //     options.UseSqlServer("YourConnectionStringHere"));

        var connectionString = configuration.GetConnectionString("Database");

        //// Add services to the container.
        //services.AddDbContext<OrderingContext>(options =>
        //     options.UseSqlServer(connectionString));
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}

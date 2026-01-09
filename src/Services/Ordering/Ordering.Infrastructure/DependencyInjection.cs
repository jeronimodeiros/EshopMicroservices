using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

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
  //      services.AddDbContext<IApplicationDbContext>((sp, options) =>
  //      {
  //          options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
		//	options.UseSqlServer(connectionString);
		//});
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}

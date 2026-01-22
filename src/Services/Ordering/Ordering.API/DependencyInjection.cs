using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register API services here
        // Example: services.AddControllers();
        // services.AddSwaggerGen();

        // Add other necessary services for the API layer
        // services.AddAuthentication();
        // services.AddAuthorization();
        services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure API middleware here
        // Example: app.UseSwagger();
        // app.UseSwaggerUI();
        // Add other necessary middleware for the API layer
        // app.UseAuthentication();
        // app.UseAuthorization();
        // app.MapControllers();

        // Add Carter endpoints if using Carter
        app.MapCarter();
        return app;
    }
}

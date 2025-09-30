using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extentions;

public static class DatabaseExtentions
{
	public static async Task InitialiseDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		context.Database.MigrateAsync().GetAwaiter().GetResult(); // Ensure database is created and apply migrations

		await SeedAsync(context);
	}

	private static async Task SeedAsync(ApplicationDbContext context)
	{
		await SeedCustomerAsync(context);
		//await SeedProductAsync(context);
		//await SeedOrderandItemsAsync(context);

	}

	private static async Task SeedCustomerAsync(ApplicationDbContext context)
	{
		if (!await context.Customers.AnyAsync())
		{
			await context.Customers.AddRangeAsync(InitialData.Customers);
			await context.SaveChangesAsync();
		}
	}
}

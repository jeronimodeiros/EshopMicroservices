using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System.Reflection;
using System.Reflection.Emit;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Customer> Customers => Set<Customer>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<OrderItem> OrderItems => Set<OrderItem>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Order>(orderBuilder =>
		{
			// Map OrderId.Value as the PK
			orderBuilder.HasKey(o => o.Id);
			orderBuilder.Property(o => o.Id)
				.HasConversion(
					id => id.Value, // OrderId to Guid
					value => OrderId.Of(value)) // Guid to OrderId
				.ValueGeneratedNever(); // If you want to control the value externally
			orderBuilder.Ignore(o => o.TotalPrice);
		});

		builder.Entity<OrderItem>(itemBuilder =>
		{
			itemBuilder.HasKey(oi => oi.Id);

			// Map OrderId as FK to Order.Id.Value
			itemBuilder.Property(oi => oi.OrderId).IsRequired();

			itemBuilder.HasOne<Order>()
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId)
				.HasPrincipalKey(o => o.Id); // Match FK type (Guid) to PK type (Guid)
		});
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(builder);
	}
}

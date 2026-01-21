using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.HasKey(oi => oi.Id);
		builder.Property(oi => oi.Id).HasConversion(
				orderItemId => orderItemId.Value,
				dbId => OrderItemId.Of(dbId));

		builder.Property(oi => oi.OrderId).HasConversion(
			id => id.Value,
			db => OrderId.Of(db));
		builder.Property(oi => oi.ProductId).HasConversion(
			id => id.Value,
			db => ProductId.Of(db));

		builder.HasOne<Product>()
			.WithMany()
			.HasForeignKey(oi => oi.ProductId);

		builder.Property(oi => oi.Quantity).IsRequired();

		builder.Property(oi => oi.Price).IsRequired();
	}
}

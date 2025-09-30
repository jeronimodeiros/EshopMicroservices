namespace Ordering.Infrastructure.Data.Extentions;

internal class InitialData
{
	public static IEnumerable<Customer> Customers => new List<Customer>
	{
		Customer.Create(CustomerId.Of(new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851")), "John Doe", "john@gmail.com"),
		Customer.Create(CustomerId.Of(new Guid("c56a4180-65aa-42ec-a945-5fd21dec0538")), "Jane Smith", "janes.smith@gmail.com")
	};
}

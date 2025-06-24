namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
}

namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;
    public string Country { get; } = default!;
    protected Address() { }
    private Address(string firstName, string lastName, string? emailAddress, string addressLine, string state, string zipCode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
    public static Address Of(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string state,
        string zipCode,
        string country)
    {
        //ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        //ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
        //ArgumentException.ThrowIfNullOrWhiteSpace(state, nameof(state));
        //ArgumentException.ThrowIfNullOrWhiteSpace(zipCode, nameof(zipCode));
        //ArgumentException.ThrowIfNullOrWhiteSpace(country, nameof(country));
        return new Address(firstName, lastName, emailAddress, addressLine, state, zipCode, country);
    }
}

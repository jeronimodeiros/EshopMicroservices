namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardNumber { get; } = default!;
    public string CardName { get; } = default!;
    public string ExpirationDate { get; } = default!;
    public string Cvv { get; } = default!;
    public int PaymentMethod { get; } = default!; // 1: Credit Card, 2: Debit Card, 3: PayPal, etc.

    protected Payment() { }
    private Payment(string cardNumber, string cardName, string expirationDate, string cvv, int paymentMethod)
    {
        CardNumber = cardNumber;
        CardName = cardName;
        ExpirationDate = expirationDate;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(
        string cardNumber,
        string cardName,
        string expirationDate,
        string cvv,
        int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        //ArgumentException.ThrowIfNullOrWhiteSpace(expirationDate, nameof(expirationDate));
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
                
        return new Payment(cardNumber, cardName, expirationDate, cvv, paymentMethod);
    }
}

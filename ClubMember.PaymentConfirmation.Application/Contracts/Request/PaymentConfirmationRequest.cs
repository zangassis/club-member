namespace ClubMember.PaymentConfirmation.Application.Contracts.Request;
public class PaymentConfirmationRequest
{
    public PaymentConfirmationRequest(string? userId, string? invoiceCode)
    {
        UserId = userId;
        InvoiceCode = invoiceCode;
    }

    public string? UserId { get; set; }
    public string? InvoiceCode { get; set; }
}
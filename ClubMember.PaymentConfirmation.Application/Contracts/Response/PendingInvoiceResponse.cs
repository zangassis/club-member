namespace ClubMember.PaymentConfirmation.Application.Contracts.Response;
public class PendingInvoiceResponse
{
    public PendingInvoiceResponse(string? userId, string? invoiceCode)
    {
        UserId = userId;
        InvoiceCode = invoiceCode;
    }

    public string? UserId { get; set; }
    public string? InvoiceCode { get; set; }
}

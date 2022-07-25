namespace ClubMember.PaymentConfirmation.Domain.Entities;
public class MemberInvoice : BaseEntity
{
    public MemberInvoice(string? userId, string? invoiceCode, string? status)
    {
        UserId = userId;
        InvoiceCode = invoiceCode;
        Status = status;
    }

    public string? UserId { get; set; }
    public string? InvoiceCode { get; set; }
    public string? Status { get; set; }
}
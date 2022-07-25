using ClubMember.PaymentConfirmation.Application.Contracts.Request;
using ClubMember.PaymentConfirmation.Application.Contracts.Response;
using ClubMember.PaymentConfirmation.Application.Contracts.Shared.Response;

namespace ClubMember.PaymentConfirmation.Application.Interfaces;
public interface IPaymentConfirmationService
{
    public Task<GenericResponse> PaymentConfirmation(PaymentConfirmationRequest request);
    public Task<bool> PaymentConfirmationVerify();
    public Task<bool> RamdomPaymentVerify();
    public Task<GenericResponse> ResendPendingInvoices(PaymentConfirmationRequest request);
    public Task<List<PendingInvoiceResponse>> GetAllPendingInvoices();
}
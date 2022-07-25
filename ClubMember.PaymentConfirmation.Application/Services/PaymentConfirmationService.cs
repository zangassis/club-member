using ClubMember.PaymentConfirmation.Application.Contracts.Request;
using ClubMember.PaymentConfirmation.Application.Contracts.Response;
using ClubMember.PaymentConfirmation.Application.Contracts.Shared.Response;
using ClubMember.PaymentConfirmation.Application.Interfaces;
using ClubMember.PaymentConfirmation.Domain.Entities;
using ClubMember.PaymentConfirmation.Infrastructure.DBContext;
using Microsoft.Extensions.Logging;

namespace ClubMember.PaymentConfirmation.Application.Services;
public class PaymentConfirmationService : IPaymentConfirmationService
{
    private readonly PaymentConfirmationDBContext _paymentConfContext;
    private readonly ILogger<PaymentConfirmationService> _logger;

    public PaymentConfirmationService(
        PaymentConfirmationDBContext paymentConfContext,
        ILogger<PaymentConfirmationService> logger)
    {
        _paymentConfContext = paymentConfContext;
        _logger = logger;
    }

    public async Task<GenericResponse> PaymentConfirmation(PaymentConfirmationRequest request)
    {
        try
        {
            bool confirmationPayment = await PaymentConfirmationVerify();

            var memberInvoiceEntity = new MemberInvoice(
                request.UserId,
                request.InvoiceCode,
                confirmationPayment == true ? "paid" : "pending");

            await _paymentConfContext.AddAsync(memberInvoiceEntity);
            await _paymentConfContext.SaveChangesAsync();

            return GenericResponse.Result(true, "Success saving MemberInvoice entity");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error saving MemberInvoice entity - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    public async Task<bool> PaymentConfirmationVerify()
    {
        var confirmationPayment = await RamdomPaymentVerify();

        return Task.FromResult(confirmationPayment).Result;
    }

    public Task<bool> RamdomPaymentVerify()
    {
        int minValue = 1;
        int maxValue = 100;
        var random = new Random();
        int randomValue = random.Next(minValue, maxValue + 1);

        if (randomValue > 20 && randomValue < 60)
            return Task.FromResult(true);
        else return Task.FromResult(false);
    }

    public Task<List<PendingInvoiceResponse>> GetAllPendingInvoices()
    {
        var pendingInvoices = _paymentConfContext.MemberInvoices?.Where(x => x.Status == "pending");

        var response = pendingInvoices?.Select(p => new PendingInvoiceResponse(p.UserId, p.InvoiceCode)).ToList();

        return Task.FromResult<List<PendingInvoiceResponse>>(response);
    }

    public Task<GenericResponse> ResendPendingInvoices(PaymentConfirmationRequest request)
    {
        try
        {
            var memberEntiy = _paymentConfContext?.MemberInvoices?.FirstOrDefault(m => m.UserId == request.UserId);

            if (memberEntiy is null)
                return Task.FromResult(GenericResponse.Result(false, "Member not found"));

            var confirmationPayment = RamdomPaymentVerify().Result;

            memberEntiy.Status = confirmationPayment is true ? "paid" : "pending";

            _paymentConfContext?.MemberInvoices?.Update(memberEntiy);
            _paymentConfContext?.SaveChanges();

            return Task.FromResult(GenericResponse.Result(true, "Success updating MemberInvoice entity"));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating MemberInvoice entity - ", ex.Message);
            return Task.FromResult(GenericResponse.Result(false, ex.Message));
        }
    }
}

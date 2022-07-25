using ClubMember.PaymentConfirmation.Application.Contracts.Request;
using ClubMember.PaymentConfirmation.Application.Contracts.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace ClubMember.ResendPaymentConfirmation.Application.Services;
public class ResendPaymentConfirmationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ResendPaymentConfirmationService> _logger;

    public ResendPaymentConfirmationService(HttpClient httpClient, ILogger<ResendPaymentConfirmationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> ResendPaymentConfirmation()
    {
        try
        {
            var pendingInvoices = GetPendingIvoices().Result;

            if (!pendingInvoices.Any())
            {
                _logger.LogInformation("No records to process");
                return true;
            }
            return await ExecuteResendPendingInvoices(pendingInvoices);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error IN ResendPaymentConfirmation - {ex.Message}");
            return false;
        }
    }

    public async Task<List<PendingInvoiceResponse>> GetPendingIvoices()
    {
        const string uriPendingInvoices = "/PaymentConfirmation/pending-invoices";

        var responseString = await _httpClient.GetStringAsync(uriPendingInvoices);

        return JsonConvert.DeserializeObject<List<PendingInvoiceResponse>>(responseString);
    }

    public async Task<bool> ExecuteResendPendingInvoices(List<PendingInvoiceResponse> pendingInvoices)
    {
        bool success = true;

        foreach (var invoice in pendingInvoices)
        {
            const string uriUpdateInvoices = "/PaymentConfirmation/resend-pending-invoices";
            var requestItem = new PaymentConfirmationRequest(invoice.UserId, invoice.InvoiceCode);

            var dataAsString = JsonConvert.SerializeObject(requestItem);

            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            var result = await _httpClient.PutAsync(uriUpdateInvoices, content);

            success = result.IsSuccessStatusCode;
        }
        return success;
    }
}


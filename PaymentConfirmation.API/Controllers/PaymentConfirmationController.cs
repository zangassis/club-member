using ClubMember.PaymentConfirmation.Application.Contracts.Request;
using ClubMember.PaymentConfirmation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PaymentConfirmation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentConfirmationController : ControllerBase
    {
        private readonly IPaymentConfirmationService _paymentConfService;

        public PaymentConfirmationController(IPaymentConfirmationService paymentConfService)
        {
            _paymentConfService = paymentConfService;
        }

        [HttpPost("payment-confirmation")]
        public async Task<ActionResult> PaymentConfirmation(PaymentConfirmationRequest request)
        {
            var result = await _paymentConfService.PaymentConfirmation(request);

            return result.Success == true ? Ok(result) : BadRequest(result);
        }

        [HttpPut("resend-pending-invoices")]
        public async Task<ActionResult> ResendPendingInvoices(PaymentConfirmationRequest request)
        {
            var result = await _paymentConfService.ResendPendingInvoices(request);

            return result is not null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("pending-invoices")]
        public async Task<ActionResult> GetAllPendingInvoices()
        {
            var result = await _paymentConfService.GetAllPendingInvoices();

            return result is not null ? Ok(result) : NotFound();
        }
    }
}

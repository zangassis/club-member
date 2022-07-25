using ClubMember.ResendPaymentConfirmation.Application.Services;

namespace ClubMember.ResendPaymentConfirmation.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ResendPaymentConfirmationService _resendService;

        public Worker(ILogger<Worker> logger, ResendPaymentConfirmationService resendService)
        {
            _logger = logger;
            _resendService = resendService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                bool result = await _resendService.ResendPaymentConfirmation();

                _logger.LogInformation($"End process - Success: {result}");

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
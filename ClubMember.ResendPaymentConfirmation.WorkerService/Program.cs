using ClubMember.ResendPaymentConfirmation.Application.Services;
using ClubMember.ResendPaymentConfirmation.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient<ResendPaymentConfirmationService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:LOCALPORTNUMBER/");
        });
    })
    .Build();

await host.RunAsync();

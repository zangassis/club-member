namespace ClubMember.PaymentConfirmation.Application.Contracts.Shared.Response;
public class GenericResponse
{
    public bool Success { get; set; }
    public string? ResultMessage { get; set; }

    public GenericResponse(bool success, string? resultMessage)
    {
        Success = success;
        ResultMessage = resultMessage;
    }

    public static GenericResponse Result(bool success, string resultMessage) =>
        new GenericResponse(success, resultMessage);
}
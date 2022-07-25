namespace ClubMember.PaymentConfirmation.Domain.Entities;
public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}

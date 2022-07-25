using ClubMember.PaymentConfirmation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubMember.PaymentConfirmation.Infrastructure.DBContext;

public class PaymentConfirmationDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection = "server=localhost; database=clubMember; user=YOURUSER; password=YOURPASSWORD";

        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }

    public DbSet<MemberInvoice>? MemberInvoices { get; set; }
}
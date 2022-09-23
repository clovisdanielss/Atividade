namespace Atividade.Models
{
    public class Sale
    {
        public string? Id { get; set; }
        public Cart? Cart { get; set; }
        public Salesman? Salesman { get; set; }
        public DateTime SalesDate { get; set; }
        public SaleStatus Status { get; set; }

    }

    public enum SaleStatus
    {
        WAITING_PAYMENT,
        PAYMENT_APPROVED,
        SENT,
        DELIVERED,
        CANCELLED
    }
}
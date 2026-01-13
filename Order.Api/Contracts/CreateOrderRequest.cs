namespace Order.Api.Contracts
{
    public record CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
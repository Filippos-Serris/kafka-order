namespace Shared.Contracts.Events
{
    /// <summary>
    /// Event representing the creation of a new order.
    /// </summary>
    public record CreateOrderEvent
    {
        /// <summary>
        /// The unique identifier for the order.
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// The unique identifier for the customer placing the order.
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Total monetary value of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// The date and time when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
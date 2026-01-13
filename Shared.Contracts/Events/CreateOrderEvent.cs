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
        public Guid OrderId { get; init; }
        /// <summary>
        /// The unique identifier for the customer placing the order.
        /// </summary>
        public Guid CustomerId { get; init; }
        /// <summary>
        /// Total monetary value of the order.
        /// </summary>
        public decimal TotalAmount { get; init; }
        /// <summary>
        /// The date and time when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }
    }
}
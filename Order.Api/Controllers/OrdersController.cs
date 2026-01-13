using Microsoft.AspNetCore.Mvc;
using Order.Api.Application;
using Order.Api.Contracts;
using Shared.Contracts.Events;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderEventPublisher _publisher;
        public OrdersController(IOrderEventPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var orderEvent = new CreateOrderEvent
            {
                OrderId = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount,
                CreatedAt = DateTime.UtcNow
            };

            await _publisher.PublishCreatedOrderEvntAsync(orderEvent, cancellationToken);

            return Accepted(new { orderEvent.OrderId });
        }
    }
}
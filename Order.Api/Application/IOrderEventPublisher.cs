using Shared.Contracts.Events;

namespace Order.Api.Application
{
    public interface IOrderEventPublisher
    {
        Task PublishCreatedOrderEventAsync(CreateOrderEvent orderEvent, CancellationToken cancellationToken);
    }
}
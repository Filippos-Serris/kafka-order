using Shared.Contracts.Events;

namespace Order.Api.Application
{
    public interface IOrderEventPublisher
    {
        Task PublishCreatedOrderEvntAsync(CreateOrderEvent orderEvent, CancellationToken cancellationToken);
    }
}
using MediatR;
using Order.Consumer.Application.Commands;

namespace Order.Consumer.Application.Handlers
{
    public class OrderHandler : IRequestHandler<OrderCommand>
    {
        public Task Handle(OrderCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
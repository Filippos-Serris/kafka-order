using MediatR;
using Shared.Contracts.Events;

namespace Order.Consumer.Application.Commands
{
    public class OrderCommand : IRequest
    {
        public CreateOrderEvent Order { get; }

        public OrderCommand(CreateOrderEvent order)
        {
            Order = order;
        }
    }
}
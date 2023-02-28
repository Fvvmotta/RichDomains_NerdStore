using MediatR;

namespace NerdStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<InitiatedOrderDraftEvent>,
        INotificationHandler<UpdatedOrderEvent>,
        INotificationHandler<AddedItemToOrderEvent>
    {
        public Task Handle(InitiatedOrderDraftEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
       
        public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(AddedItemToOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

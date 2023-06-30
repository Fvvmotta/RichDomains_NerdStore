using MediatR;

namespace NerdStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<InitiatedOrderDraftEvent>,
        INotificationHandler<UpdatedOrderEvent>,
        INotificationHandler<AddedItemOrderEvent>
    {
        public Task Handle(InitiatedOrderDraftEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
       
        public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(AddedItemOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

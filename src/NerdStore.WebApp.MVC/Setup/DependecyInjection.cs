using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Data;
using NerdStore.Sales.Data.Repository;
using NerdStore.Sales.Domain;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependecyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatrHandler>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowInventoryEvent>, ProductEventHandler>();

            //sales
            services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
            //services.AddScoped<IRequestHandler<InitiateOrderCommand, bool>, OrderCommandHandler>();
            //services.AddScoped<IRequestHandler<FinalizeOrderCommand, bool>, OrderCommandHandler>();
            //services.AddScoped<IRequestHandler<CancelProcessOrderCommand, bool>, OrderCommandHandler>();
            //services.AddScoped<IRequestHandler<CancelProcessOrderReturnInventoryCommand, bool>, OrderCommandHandler>();


            services.AddScoped<INotificationHandler<InitiatedOrderDraftEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<AddedItemOrderEvent>, OrderEventHandler>();
            
            return services;
        }
    }
}

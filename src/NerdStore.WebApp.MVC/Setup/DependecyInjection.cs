using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Data.Repository;
using NerdStore.Sales.Domain;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependecyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatrHandler>();

            //Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowInventoryEvent>, ProductEventHandler>();

            //sales
            services.AddScoped<IRequestHandler<AddItemToOrderCommand, bool>, OrderCommandHandler>();

            return services;
        }
    }
}

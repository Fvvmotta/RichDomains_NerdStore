using NerdStore.Sales.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartViewModel> GetClientCart(Guid clientId);
        Task<IEnumerable<OrderViewModel>> GetClientOrders(Guid clientId);
    }
}

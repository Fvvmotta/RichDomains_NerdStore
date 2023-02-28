using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public int Code { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime RegisterDate { get; set; }
        public int OrderStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using Order = Models.Order;
    using OrderItem = Models.OrderItem;

    public class DetailsViewModel
    {
        public Order Order { get; set; }
    }
}
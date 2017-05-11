using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using OrderItem = Models.OrderItem;
    using Order = Models.Order;

    public class DeleteViewModel
    {
        public OrderItem OrderItem { get; set; }
        public Order Order { get; set; }
    }
}
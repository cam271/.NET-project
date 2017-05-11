using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using OrderItem = Models.OrderItem;
    using Item = Models.Item;

    public class CreateItemViewModel
    {
        public OrderItem OrderItem { get; set; } 
        public int IdOrItemNumber { get; set; }
    }
}
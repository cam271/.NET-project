using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using OrderItem = Models.OrderItem;

    public class EditItemViewModel
    {
        public OrderItem OrderItem { get; set; }
    }
}
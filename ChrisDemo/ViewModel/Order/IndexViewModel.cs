using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using Order = Models.Order;

    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
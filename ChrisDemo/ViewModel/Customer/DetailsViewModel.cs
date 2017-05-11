using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Customer
{
    using Customer = Models.Customer;
    using Order = Models.Order;

    public class DetailsViewModel
    {
        public Customer Customer { get; set; }
        //public IEnumerable<Order> Orders { get; set; }
    }
}
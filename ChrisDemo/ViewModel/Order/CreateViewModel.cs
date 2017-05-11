using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Order
{
    using Order = Models.Order;
    using Location = Models.Location;
    using Customer = Models.Customer;

    public class CreateViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
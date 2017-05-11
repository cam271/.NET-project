using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Customer
{
    using Customer = Models.Customer;

    public class IndexViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
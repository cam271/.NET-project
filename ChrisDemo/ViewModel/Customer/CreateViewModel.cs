using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Customer
{
    using Customer = Models.Customer;
    using Location = Models.Location;
    using State = Models.State;

    public class CreateViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Location> Locations { get; set; }   
        public IEnumerable<State> States { get; set; }
    }
}
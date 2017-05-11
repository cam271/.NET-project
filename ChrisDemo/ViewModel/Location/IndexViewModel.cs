using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChrisDemo.Models;

namespace ChrisDemo.ViewModel.Location
{
    using Location = Models.Location;

    public class IndexViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}
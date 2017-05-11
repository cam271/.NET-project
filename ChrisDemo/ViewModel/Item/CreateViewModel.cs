using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisDemo.ViewModel.Item
{
    using Item = Models.Item;
    using Category = Models.Category;
    using Location = Models.Location;

    public class CreateViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IList<int> SelectedLocationIds { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Location
{

    using Location = Models.Location;
    using Item = Models.Item;

    public class EditViewModel
    {
        public Location Location { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IList<int> SelectedItemIds { get; set; }
    }
}
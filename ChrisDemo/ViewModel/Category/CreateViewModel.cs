using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Category
{
    using Category = Models.Category;
    using Item = Models.Item;

    public class CreateViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IList<int> SelectedItemIds { get; set; }
    }
}
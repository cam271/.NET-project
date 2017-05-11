using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Item
{
    using Item = Models.Item;

    public class IndexViewModel
    {
        public IEnumerable<Item> Items { get; set; }    
    }
}
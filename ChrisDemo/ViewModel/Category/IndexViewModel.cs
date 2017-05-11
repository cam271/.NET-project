using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisDemo.ViewModel.Category
{
    using Category = Models.Category;

    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
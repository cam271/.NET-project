using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChrisDemo.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Item Item { get; set; }
    }
}
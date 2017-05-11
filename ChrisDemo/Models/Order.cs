using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChrisDemo.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual Location Location { get; set; }
    }
}
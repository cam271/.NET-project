using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisDemo.Models
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [StringLength(5)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Zip code can only contain numbers.")]
        public string Zip { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Location Location { get; set; }
    }
}
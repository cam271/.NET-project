using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChrisDemo.Models
{
    [Table("Item")]
    public class Item
    {
        public Item()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Item No.")]
        public string ItemNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual Category Category { get; set; }
    }
}
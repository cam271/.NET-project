using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChrisDemo.Models
{
    [Table("Location")]
    public class Location
    {
        public Location()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
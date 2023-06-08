using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NIMAP2.Models
{
    public class CategoryList
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Products")]
        public int ProductId { get; set; }


        [Key, Column(Order = 1)]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public bool IsActive { get; set; }

        public Category Categories { get; set; }

        public Product Products { get; set; }
    }
}
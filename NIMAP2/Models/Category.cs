using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NIMAP2.Models
{
    public class Category
    {
        [Key]
        [DisplayName("ID")]
        public int Id { get; set; }


        public string CategoryName { get; set; }

        public bool IsActive { get; set; }
    }
}
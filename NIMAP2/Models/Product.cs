using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NIMAP2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required !!!")]
        [DisplayName("Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description is required !!!")]
        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required!!!")]
        [DisplayName("Price")]
        public int ProductPrice { get; set; }


        [ForeignKey("SignupTbl")]
        public int UserId { get; set; }

        public SignUp SignupTbl { get; set; }

    }
}
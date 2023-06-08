using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NIMAP2.Models
{
    [Table("SignupTbl")]
    public class SignUp
    {
        [Key]
        public int id { get; set; }


        [DisplayName("Email")]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email")]
        [Required]
        public string UserEmail { get; set; }


        [StringLength(50)]
        [Required]
        public string UserName { get; set; }


        [DisplayName("Password")]
        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.Admin.Models.Users
{
    public class UserModel
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public int? UserTypeID { get; set; }

        public int? ImageID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

       
        public int? OrganizationID { get; set; }

        public byte[] FileBinary { get; set; }
    }
}
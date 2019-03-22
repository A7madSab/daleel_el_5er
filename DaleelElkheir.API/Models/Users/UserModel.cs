using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaleelElkheir.API.Models.Users
{
    public class UserModel:BaseRequest
    {
        public int UserID { get; set; }
    }
    public class FavoriteCategoryModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int CategoryID { get; set; }
    }

    public class FavoriteOrganizationModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int OrganizationID { get; set; }
    }

    public class ParticipateCasesModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int CaseID { get; set; }
    }

    public class ForgetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }

    public class VerifyCodeRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public int code { get; set; }
    }

    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Code { get; set; }

    }

    public class EditProfileModel:BaseRequest
    {

        [Required]
        public int UserID { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Facebook_ID { get; set; }

        public string Google_ID { get; set; }
        public int ImageID { get; set; }
        public string Image { get; set; }

        public byte[] Image2 { get; set; }
    }
}
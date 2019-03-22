using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.API.Models.Users
{
    public class ContactRequest : BaseRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        public string DonorIn { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }
    }

    public class SubscribeRequest
    {

        [Required]
        public string Email { get; set; }

    }

    public class DeviceTokenRequest
    {
        [Required]
        [StringLength(500)]
        public string DeviceTokenKey { get; set; }

    }

    public class UpdateDeviceTokenRequest
    {
        [Required]
        public Guid UserKey { get; set; }
        [Required]
        [StringLength(500)]
        public string DeviceTokenKey { get; set; }
       
    }
    public class RemoveTokenRequest
    {
        [Required]
        public Guid UserKey { get; set; }

    }
}

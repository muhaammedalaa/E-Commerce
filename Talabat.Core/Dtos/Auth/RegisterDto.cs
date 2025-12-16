using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name  Is Reqired")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Email Is Reqired")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Reqired")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone Number Is Reqired")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}

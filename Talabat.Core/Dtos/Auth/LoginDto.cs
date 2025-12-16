using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email Is Reqired")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Reqired")]
        public string Password { get; set; }
    }
}

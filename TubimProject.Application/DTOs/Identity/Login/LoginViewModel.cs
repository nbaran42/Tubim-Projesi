using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Identity.Login
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı Adı Girilmesi Mecburidir.")]

        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre Girilmesi Mecburidir.")]
        public string Password { get; set; }
        public Uri ReturnUrl { get; set; }
    }
}

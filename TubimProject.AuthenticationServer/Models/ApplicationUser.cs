using Microsoft.AspNetCore.Identity;
using System;

namespace TubimProject.AuthenticationServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        public int KurumId { get; set; }
        public string KurumDaire { get; set; }
        public string CepTel { get; set; }
        public string Unvan { get; set; }
        public DateTime T_Islem { get; set; }
        public bool Aktifmi { get; set; }
    }
}

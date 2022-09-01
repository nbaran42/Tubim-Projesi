using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Identity.User
{
    public class AddNewUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı Adı Mecburidr")]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string CepTel { get; set; }
        public string KurumDaire { get; set; }
        public int KurumId { get; set; }
        public string PhoneNumber { get; set; }
        public string Unvan { get; set; }
        public bool Aktifmi => true;
        public DateTime T_Islem => DateTime.Now;

    }
}

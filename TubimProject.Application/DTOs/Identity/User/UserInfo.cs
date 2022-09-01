using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Identity.User
{
    public class UserInfo
    {
        public int KurumId { get; set; }
        public string KurumDaire { get; set; }
        public string CepTel { get; set; }
        public string Unvan { get; set; }
        public DateTime T_Islem { get; set; }
        public bool Aktifmi { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels
{
    public class JGetToken
    {
        public string kullaniciAdi { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;

    }
}

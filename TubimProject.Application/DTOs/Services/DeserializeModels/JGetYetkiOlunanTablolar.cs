using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels
{
    public class JGetYetkiOlunanTablolar
    {
        public Yetkiliolunantablolistesi? yetkiliOlunanTabloListesi { get; set; }
    }
    public class Yetkiliolunantablolistesi
    {
        public int sonucKod { get; set; }
        public string? sonucAck { get; set; }
        public Sonuclist[]? sonucLists { get; set; }
    }
    public class Sonuclist
    {
        public int tabloTanimId { get; set; }
        public string? tabloAciklamasi { get; set; }
        public string? taploTipi { get; set; }
        public string? tabloIliskiAciklamasi { get; set; }
    }
}

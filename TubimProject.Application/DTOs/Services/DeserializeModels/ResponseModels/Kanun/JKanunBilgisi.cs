using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Kanun
{
    public class JKanunBilgisi
    {
        public Aktarilacaklarlistesi aktarilacaklarListesi { get; set; }
    }


    public class Aktarilacaklarlistesi
    {
        public int sonucKod { get; set; }
        public string sonucAck { get; set; }
        public Sonuclist[] sonucList { get; set; }
    }

    public class Sonuclist
    {
        public int Id { get; set; }
        public int OlayNo { get; set; }
        public int KanunNo { get; set; }
        public string KanunBilgisi { get; set; }
        public string Durum { get; set; }
        public DateTime LogTarihi { get; set; }
    }
}

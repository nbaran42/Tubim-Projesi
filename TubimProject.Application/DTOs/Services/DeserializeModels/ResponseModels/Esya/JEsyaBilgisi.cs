using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Esya
{
    public class JEsyaBilgisi
    {
        public aktarilacaklarListesi aktarilacaklarListesi { get; set; }
    }
    public class aktarilacaklarListesi
    {
        public int sonucKod { get; set; }
        public string sonucAck { get; set; }
        public Sonuclist[] sonucList { get; set; }
    }

    public class Sonuclist
    {
        public int Id { get; set; }
        public long OlayNo { get; set; }
        public long MalzemeNo { get; set; }
        public string MalzemeAdi { get; set; }
        public int? Miktari { get; set; }
        public string OlcuBirimi { get; set; }
        public string Durum { get; set; }
        public DateTime LogTarihi { get; set; }
    }
}

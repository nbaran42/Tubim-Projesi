using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Olay
{
    public class JOlayBilgisi
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
        public long? OlayNo { get; set; }
        public string Olayili { get; set; }
        public string Olayilcesi { get; set; }
        public string OlayMahallesi { get; set; }
        public string OlayKoyYer { get; set; }
        public string OlayMezraYer { get; set; }
        public string OlayBucakYer { get; set; }
        public string OlayYeri { get; set; }
        public string OlayBolgesi { get; set; }
        public DateTime OlayTarihi { get; set; }
        public string KacakcilikYontemi { get; set; }
        public string KaynakYeri { get; set; }
        public string HedefYeri { get; set; }
        public string Durum { get; set; }
        public DateTime LogTarihi { get; set; }
    }
}

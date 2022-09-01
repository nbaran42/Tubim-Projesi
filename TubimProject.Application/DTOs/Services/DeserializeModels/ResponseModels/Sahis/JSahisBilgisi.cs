using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Sahis
{
    public class JSahisBilgisi
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
        public long OlayNo { get; set; }
        public int SupheliNo { get; set; }
        public long? TcNo { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string Meslegi { get; set; }
        public string MedeniHali { get; set; }
        public string Uyrugu { get; set; }
        public string OgrenimDurumu { get; set; }
        public string Durum { get; set; }
        public DateTime LogTarihi { get; set; }
    }

}

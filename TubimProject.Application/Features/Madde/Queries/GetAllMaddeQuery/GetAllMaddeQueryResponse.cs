using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery
{
    public class GetAllMaddeQueryResponse
    {
        public long OlayId { get; set; }
        public long? JandarmaMaddeId { get; set; }
        public string? EgmMaddeId { get; set; }
        public long OlayNumarasi { get; set; }
        public string? AnaTuru { get; set; }
        public string? AltTuru { get; set; }
        public string? Birimi { get; set; }
        public decimal? Miktari { get; set; }
        public string? MalzemeNo { get; set; }
        public string? KaynakUlke { get; set; }
        public string? Durum { get; set; }
        public string? ServisAnaTuru { get; set; }
      
    }
}

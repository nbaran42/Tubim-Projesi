using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Features.Olay.Queries.GetAllOlaylar
{
    public class GetAllOlaysQueryResponse
    {
        public long? Id { get; set; }
        public long? KurumOlayId { get; set; }
        public int KurumRef { get; set; }
        public string? HedefUlkesi { get; set; }
        public string? KacakcilikYontemi { get; set; }
        public string? MudahaleEdenBirim { get; set; }
        public long? OlayNumarasi { get; set; }
        public DateTime? OlayTarihi { get; set; }
        public string? SistemNo { get; set; }
        public string? Durum { get; set; }
    }
}

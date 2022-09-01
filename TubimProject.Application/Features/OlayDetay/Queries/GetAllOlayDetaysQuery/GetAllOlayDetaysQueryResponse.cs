using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery
{
    public class GetAllOlayDetaysQueryResponse
    {
        public long? OlayId { get; set; }
        public string? OlayBeldeKoy { get; set; }
        public string? OlayIlcesi { get; set; }
        public string? OlayIli { get; set; }
        public string? OlayMahalle { get; set; }
        public string? OlayEnlem { get; set; }
        public string? OlayBoylam { get; set; }
        public string? OlayBolgesi { get; set; }
    }
}

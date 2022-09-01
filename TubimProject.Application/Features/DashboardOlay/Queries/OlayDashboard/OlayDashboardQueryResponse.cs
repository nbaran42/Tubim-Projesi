using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery;

namespace TubimProject.Application.Features.DashboardOlay.Queries.OlayDashboard
{
    public class OlayDashboardQueryResponse
    {
        public DateTime T_StartDate { get; set; }
        public DateTime T_EndDate { get; set; }
        public int GuncelOlaySayisi { get; set; }
        public string? EnCokOlayGerceklesenSehir { get; set; }
        public string? EnAzOlayGerceklesenSehir { get; set; }
        public string? OlaySayisinaGoreEncokKarsilasilanMadde { get; set; }
        public string? OlaySayisinaGoreEnazKarsilasilanMadde { get; set; }
        public List<GetAllOlaysQueryResponse>? Olaylar { get; set; }
        public List<GetAllOlayDetaysQueryResponse>? OlayDetaylar { get; set; }
        public List<GetAllMaddeQueryResponse>? Maddeler { get; set; }
    }
}

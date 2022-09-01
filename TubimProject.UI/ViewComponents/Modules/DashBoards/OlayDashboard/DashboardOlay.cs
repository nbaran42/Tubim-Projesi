using MediatR;
using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayDashboard;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.UI.Jobs.Implementations;

namespace TubimProject.UI.ViewComponents.Modules.DashBoards.OlayDashboard
{
    public class DashboardOlay : ViewComponent
    {
        private ICacheService _cacheService;
        private IMediator _mediator;
        public DashboardOlay(ICacheService cacheService, IMediator mediator)
        {
            _cacheService=cacheService;
            _mediator=mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime? t1, DateTime? t2)
        {
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            if (cacheData==null)
            {
                var s = new SetOlayCacheJob(_mediator, _cacheService);
                await s.Execute();
                cacheData=_cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            }





            OlayDashboardQueryResponse olayDashboardQueryResponse = new OlayDashboardQueryResponse();
            var olaylar = cacheData.Olaylar.Where(r => r.OlayTarihi>=t1 && r.OlayTarihi<=t2).ToList();
            var encokOlayGerceklesenSehir = cacheData.OlayDetaylar.Where(t => olaylar.Select(r => r.Id).Contains(t.OlayId)).GroupBy(r => r.OlayIli).OrderByDescending(r => r.Count()).Select(r => r.Key).FirstOrDefault();
            var enazOlayGerceklesenSehir = cacheData.OlayDetaylar.Where(t => olaylar.Select(r => r.Id).Contains(t.OlayId)).GroupBy(r => r.OlayIli).OrderBy(r => r.Count()).Select(r => r.Key).FirstOrDefault();

            var olaySayisinaGoreEnAzYakalananMadde = cacheData.Maddeler.Where(t => olaylar.Select(r => r.Id).Contains(t.OlayId)).GroupBy(r => r.AnaTuru).OrderBy(r => r.Count()).Select(r => r.Key).FirstOrDefault();
            var olaySayisinaGoreEnCokYakalananMadde = cacheData.Maddeler.Where(t => olaylar.Select(r => r.Id).Contains(t.OlayId)).GroupBy(r => r.AnaTuru).OrderByDescending(r => r.Count()).Select(r => r.Key).FirstOrDefault();

            olayDashboardQueryResponse.GuncelOlaySayisi=olaylar.Count;
            olayDashboardQueryResponse.EnCokOlayGerceklesenSehir=encokOlayGerceklesenSehir;
            olayDashboardQueryResponse.EnAzOlayGerceklesenSehir=enazOlayGerceklesenSehir;
            olayDashboardQueryResponse.OlaySayisinaGoreEnazKarsilasilanMadde=olaySayisinaGoreEnAzYakalananMadde;
            olayDashboardQueryResponse.OlaySayisinaGoreEncokKarsilasilanMadde=olaySayisinaGoreEnCokYakalananMadde;
            return View("DashboardOlay", model: olayDashboardQueryResponse);
        }
    }
}

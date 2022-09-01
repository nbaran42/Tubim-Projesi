using MediatR;
using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.Features.DashboardOlay.Queries.MaddeChartDashboard.Queries;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayChartDashboard;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayDashboard;
using TubimProject.Application.Features.KodTables.Command.Queries.GetMaddeTurleri;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.UI.Jobs.Implementations;

namespace TubimProject.UI.ViewComponents.Modules.DashBoards.MaddeChartDashboard
{
    public class MaddeChartDashboard : ViewComponent
    {
        private ICacheService _cacheService;
        private IMediator _mediator;

        public MaddeChartDashboard(ICacheService cacheService, IMediator mediator)
        {
            _cacheService=cacheService;
            _mediator=mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(DateTime? t1, DateTime? t2, string maddeAdi)
        {
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            if (cacheData==null)
            {
                var s = new SetOlayCacheJob(_mediator, _cacheService);
                await s.Execute();
                cacheData=_cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            }

            var turleri = await _mediator.Send(new GetMaddeTurleriQuery());

            if (!t1.HasValue)
                t1=DateTime.Today.AddDays(-DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            if (!t2.HasValue)
                t2=DateTime.Today;

            if (string.IsNullOrEmpty(maddeAdi))
                maddeAdi=turleri.Data.FirstOrDefault().Name;

            var olaylar = cacheData.Olaylar.Where(r => r.OlayTarihi>=t1 && r.OlayTarihi<=t2);
            var maddeler = cacheData.Maddeler;



            var result = (from a in olaylar
                          group a by a.OlayTarihi into gp
                          join b in maddeler on gp?.FirstOrDefault().Id equals b.OlayId
                          where b.AnaTuru==maddeAdi
                          let maddeAdeti = maddeler.Where(r => r.OlayId==b.OlayId).Sum(r => r.Miktari)
                          select new MaddeChartDashboardQueryResponse
                          {
                              Date = gp.Key,
                              Value=maddeAdeti,
                              MassType=b.Birimi,
                              MassName=b.AnaTuru
                          }).ToList();



            return View("MaddeChartDashboard", model: result);
        }
    }
}

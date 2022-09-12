using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TubimProject.Application.DTOs.Dashboards;
using TubimProject.Application.Features.Dashboards.Queries.OlayChartDashboard;
using TubimProject.Application.Features.Dashboards.Queries.OlayDashboard;
using TubimProject.Application.Features.KodTables.Command.Queries.GetMaddeTurleri;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.UI.Models;

namespace TubimProject.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IMediator _mediator;
        private ICacheService _cacheService;
        public HomeController(IMediator mediator, ICacheService cacheService)
        {

            _mediator = mediator;
            _cacheService = cacheService;
        }
        [Route("main/anasayfa")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult getDashBoardAction([FromQuery] DateTime? t1, [FromQuery] DateTime? t2)
        {
            return ViewComponent("DashboardOlay", new { t1, t2 });
        }

        public async Task<JsonResult> getMaddeTurleri()
        {
            var turleri = await _mediator.Send(new GetMaddeTurleriQuery());
            return Json(turleri.Data);
        }

        public async Task<IActionResult> getDashboardMaddeAction([FromQuery] DateTime? t1, [FromQuery] DateTime? t2, [FromQuery] string maddeAdi)
        {

            var olaylar = await _mediator.Send(new GetAllOlaysQuery());
            var tariharaliginaGoreOlaylar = olaylar.Data.Where(r => r.OlayTarihi>=t1 && r.OlayTarihi<=t2);


            var maddeQuery = await _mediator.Send(new GetAllMaddeQuery());

            var result = (from a in tariharaliginaGoreOlaylar
                          group a by a.OlayTarihi into gp
                          join b in maddeQuery.Data on gp?.FirstOrDefault().Id equals b.OlayId
                          where b.AnaTuru==maddeAdi
                          let maddeAdeti = maddeQuery.Data.Where(r => r.OlayId==b.OlayId).Count()
                          select new OlayChartDashboardQueryResponse
                          {
                              Date = gp.Key,
                              Value=maddeAdeti

                          }).ToList();



            return Json(result);
        }


        public IActionResult OlayMaddeInstutionsWidget()
        {
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            if (cacheData is not null)
            {
                OlayMaddeInsutitionWidgetModel olayMaddeInsutitionWidgetModel = new OlayMaddeInsutitionWidgetModel()
                {
                    OlaySayisi=cacheData.Olaylar.Count,
                    MaddeSayisi=cacheData.Maddeler.Count
                };
                return ViewComponent("TumKurumlarSayilarWidget", olayMaddeInsutitionWidgetModel);
            }
            return ViewComponent("TumKurumlarSayilarWidget");


        }
        public IActionResult BlueWidget()
        {
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            if (cacheData is not null)
            {
                OlayMaddeInsutitionWidgetModel olayMaddeInsutitionWidgetModel = new OlayMaddeInsutitionWidgetModel()
                {
                    OlaySayisi=cacheData.Olaylar.Count,
                    MaddeSayisi=cacheData.Maddeler.Count
                };
                return ViewComponent("BlueWidget", olayMaddeInsutitionWidgetModel);
            }
            return ViewComponent("BlueWidget");


        }
        
    }
}
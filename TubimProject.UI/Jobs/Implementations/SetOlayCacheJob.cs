using Microsoft.AspNetCore.SignalR;
using TubimProject.Application.Features.Dashboards.Queries.OlayChartDashboard;
using TubimProject.Application.Features.Dashboards.Queries.OlayDashboard;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery;
using TubimProject.Application.Features.Supheli.Queries.GetAllSupheli;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.Application.Interfaces.Job;
using TubimProject.Application.Interfaces.SignalR;
using TubimProject.Infrastructure.SignalR;

namespace TubimProject.UI.Jobs.Implementations
{
    public class SetOlayCacheJob : IRecurringJob
    {
        public string CronExpression => Cron.Minutely();

        public string JobId => "OlayCacheJob";

        private IMediator _mediator;
        private ICacheService _cacheService;
        private IHubContext<UpdateDashboardHubClient, IUpdateDashboardHubClient> _messageHub;
        public SetOlayCacheJob(IMediator mediator, ICacheService cacheService, IHubContext<UpdateDashboardHubClient, IUpdateDashboardHubClient> messageHub)
        {
            _mediator=mediator;
            _cacheService=cacheService;
            _messageHub=messageHub;
        }

        public async Task Execute()
        {

            OlayDashboardQueryResponse olayDashboardQueryResponses = new OlayDashboardQueryResponse();
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            var cacheChartData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard");
            var cacheSupheliChartData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("suphelichartdashboard");
            var olayData = await _mediator.Send(new GetAllOlaysQuery());
            var expirationTime = DateTimeOffset.Now.AddDays(1.0);
            if (cacheData is null || cacheData.Olaylar.Count!=olayData.Data.Count)
            {



                var olayDetay = await _mediator.Send(new GetAllOlayDetaysQuery());
                var maddeData = await _mediator.Send(new GetAllMaddeQuery());
                var supheli = await _mediator.Send(new GetAllSupheliQuery());

                olayDashboardQueryResponses.Olaylar=olayData.Data;
                olayDashboardQueryResponses.OlayDetaylar=olayDetay.Data;
                olayDashboardQueryResponses.Maddeler=maddeData.Data;
                olayDashboardQueryResponses.Supheliler=supheli.Data;
                cacheData =olayDashboardQueryResponses;
                _cacheService.SetData<OlayDashboardQueryResponse>("olaydashboard", cacheData, expirationTime);

            }

            if (cacheChartData is null)
            {

                var j = olayDashboardQueryResponses.Olaylar.GroupBy(r => r.OlayTarihi).Select(r => new OlayChartDashboardQueryResponse
                {
                    Date=r.Key,
                    Value=r.Count()
                }).ToList();

                _cacheService.SetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard", j, expirationTime);

            }

            if (cacheSupheliChartData is null)
            {
                var s = (from a in olayDashboardQueryResponses.Olaylar
                         join b in olayDashboardQueryResponses.Supheliler
                       on a.KurumOlayId equals b.OlayId
                         select new { KayitTarihi = a.OlayTarihi, KimlikNo = b.TCKimlikNoPasaportNo })
                         .GroupBy(r => r.KayitTarihi)
                         .Select(r => new OlayChartDashboardQueryResponse
                         {
                             Date=r.Key,
                             Value=r.Count()
                         }).ToList();

                _cacheService.SetData<List<OlayChartDashboardQueryResponse>>("suphelichartdashboard", s, expirationTime);
            }
            await _messageHub.Clients.All.UpdateDashboard();
        }


    }
}

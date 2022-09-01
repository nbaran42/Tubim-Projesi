using Hangfire;
using MediatR;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayChartDashboard;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayDashboard;
using TubimProject.Application.Features.ErrorLogs.Queries.GetAllLogs;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.Application.Interfaces.Job;
using TubimProject.Application.Interfaces.Repositories.Modules.Notification;
using TubimProject.UI.Jobs.Interfaces;

namespace TubimProject.UI.Jobs.Implementations
{
    public class SetOlayCacheJob : IRecurringJob
    {
        public string CronExpression => Cron.Minutely();

        public string JobId => "OlayCacheJob";

        private IMediator _mediator;
        private ICacheService _cacheService;

        public SetOlayCacheJob(IMediator mediator, ICacheService cacheService)
        {
            _mediator=mediator;
            _cacheService=cacheService;
        }

        public async Task Execute()
        {

            OlayDashboardQueryResponse olayDashboardQueryResponses = new OlayDashboardQueryResponse();
            var cacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard");
            var cacheChartData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard");


            if (cacheData is null)
            {
                var expirationTime = DateTimeOffset.Now.AddDays(1.0);

                var olayData = await _mediator.Send(new GetAllOlaysQuery());
                var olayDetay = await _mediator.Send(new GetAllOlayDetaysQuery());
                var maddeData = await _mediator.Send(new GetAllMaddeQuery());


                olayDashboardQueryResponses.Olaylar=olayData.Data;
                olayDashboardQueryResponses.OlayDetaylar=olayDetay.Data;
                olayDashboardQueryResponses.Maddeler=maddeData.Data;

                cacheData =olayDashboardQueryResponses;
                _cacheService.SetData<OlayDashboardQueryResponse>("olaydashboard", cacheData, expirationTime);
            }

            if (cacheChartData is null)
            {
                var expirationTime = DateTimeOffset.Now.AddDays(1.0);
                var j = olayDashboardQueryResponses.Olaylar.GroupBy(r => r.OlayTarihi).Select(r => new OlayChartDashboardQueryResponse
                {
                    Date=r.Key,
                    Value=r.Count()
                }).ToList();

                _cacheService.SetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard", j, expirationTime);
            }

        }


    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayChartDashboard;
using TubimProject.Application.Features.DashboardOlay.Queries.OlayDashboard;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.UI.Jobs.Implementations;

namespace TubimProject.UI.ViewComponents.Modules.DashBoards.OlayChartDashboard
{
    public class OlayChartDashboard:ViewComponent
    {
        //olaychartdashboard
        private ICacheService _cacheService;
        private IMediator _mediator;

        public OlayChartDashboard(ICacheService cacheService, IMediator mediator)
        {
            _cacheService=cacheService;
            _mediator=mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cacheData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard");
            if (cacheData==null)
            {
                var s = new SetOlayCacheJob(_mediator, _cacheService);
                await s.Execute();
                cacheData=_cacheService.GetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard");
            }
            return View("OlayChartDashboard", model: cacheData);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.Features.Dashboards.Queries.OlayChartDashboard;
using TubimProject.Application.Interfaces.Cache;

namespace TubimProject.UI.ViewComponents.Modules.DashBoards.SupheliChartDashboard
{
    public class SupheliChartDashboard:ViewComponent
    {
        private ICacheService _cacheService;
        private IMediator _mediator;

        public SupheliChartDashboard(ICacheService cacheService, IMediator mediator)
        {
            _cacheService=cacheService;
            _mediator=mediator;
        }

        public IViewComponentResult Invoke()
        {
            var cacheData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("suphelichartdashboard");

            return View("SupheliChartDashboard", model: cacheData);
        }
    }
}

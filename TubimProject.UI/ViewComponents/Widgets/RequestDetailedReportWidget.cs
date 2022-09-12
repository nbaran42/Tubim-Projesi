using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Widgets
{
    public class RequestDetailedReportWidget:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("RequestDetailedReportWidget");  
        }
    }
}

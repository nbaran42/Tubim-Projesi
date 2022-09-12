using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Modules.DashBoards
{
    public class DashboardSupheli:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("DashboardSupheli");
        }
    }
}

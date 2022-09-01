using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Menus
{
    public class mnLogs:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("mnLogs");
        }
    }
}

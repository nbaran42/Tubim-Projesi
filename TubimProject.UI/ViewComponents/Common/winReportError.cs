using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Common
{
    public class winReportError:ViewComponent
    {
        public IViewComponentResult Invoke(string username)
        {
            TempData["username"]=username;
            return View("winReportError");
        }
    }
}

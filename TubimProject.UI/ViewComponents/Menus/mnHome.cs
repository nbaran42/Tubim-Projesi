using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Menus
{
    public class mnHome : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("mnHome");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Menus
{
    public class mnYonetici:ViewComponent
    {
        public  IViewComponentResult Invoke()
        {
            return View("mnYonetici");
        }
    }
}

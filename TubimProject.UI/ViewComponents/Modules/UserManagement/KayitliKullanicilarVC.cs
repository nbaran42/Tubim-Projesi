using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Modules.UserManagement
{
    public class KayitliKullanicilarVC:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("ButunKullanicilar");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Modules.RoleManagement
{
    public class KayitliRollerVC : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Roller");
        }
    }
}

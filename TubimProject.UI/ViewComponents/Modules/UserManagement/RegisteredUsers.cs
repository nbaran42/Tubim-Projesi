using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Modules.UserManagement
{
    public class RegisteredUsers : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("AllUsers");
        }
    }
}

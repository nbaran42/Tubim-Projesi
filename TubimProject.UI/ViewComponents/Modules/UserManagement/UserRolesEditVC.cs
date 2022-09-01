using Microsoft.AspNetCore.Mvc;

namespace TubimProject.UI.ViewComponents.Modules.UserManagement
{
    public class UserRolesEditVC:ViewComponent
    {
        public IViewComponentResult Invoke(string username,IEnumerable<string> roles)
        {
            TempData.Put<IEnumerable<string>>("userroles",roles);
            TempData["username"]=username;
            return View("EditUserRoles");
        }
    }
}

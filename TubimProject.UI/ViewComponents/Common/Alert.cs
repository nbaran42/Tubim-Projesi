using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Common;

namespace TubimProject.UI.ViewComponents.Common
{
    public class Alert:ViewComponent
    {
        public IViewComponentResult Invoke(AlertViewModel model)
        {
            return View("Alert", model: model);
        }
    }
}

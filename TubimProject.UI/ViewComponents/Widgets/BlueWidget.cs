using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Dashboards;

namespace TubimProject.UI.ViewComponents.Widgets
{
    public class BlueWidget:ViewComponent
    {

        public IViewComponentResult Invoke(OlayMaddeInsutitionWidgetModel model)
        {
            return View("BlueWidget", model: model);
        }
    }
}

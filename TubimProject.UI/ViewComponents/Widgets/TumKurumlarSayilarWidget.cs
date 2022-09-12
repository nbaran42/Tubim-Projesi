using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Dashboards;

namespace TubimProject.UI.ViewComponents.Widgets
{
    public class TumKurumlarSayilarWidget : ViewComponent
    {
        public IViewComponentResult Invoke(OlayMaddeInsutitionWidgetModel model)
        {
            return View("TumKurumlarSayilarWidget", model);
        }
    }
}

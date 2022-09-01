using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Identity.User;
using TubimProject.UI.Services;

namespace TubimProject.UI.ViewComponents.Modules.TopRightUserInfo
{
    public class LoggedinUser : ViewComponent
    {
        private readonly IApiResourceHttpClient apiResourceHttpClient;

        public LoggedinUser(IApiResourceHttpClient apiResourceHttpClient)
        {
            this.apiResourceHttpClient=apiResourceHttpClient;
        }

        public async Task< IViewComponentResult> InvokeAsync(string username)
        {
            var result =await apiResourceHttpClient.GetUserInformation(username);
            if (result.isSuccess)
            {
                var userInfo = (UserInfo)result.Value;
                return View("LoggedinUser", model: userInfo);
            }
            return View("LoggedinUser");
        }
    }
}

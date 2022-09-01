using System.Security.Claims;
using TubimProject.Application.Interfaces.Shared;

namespace TubimProject.UI.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            Username = httpContextAccessor.HttpContext?.User?.FindFirst("name")== null ? null : httpContextAccessor.HttpContext?.User?.FindFirst("name").Value;
        } 
        public string Username { get; }
    }
}

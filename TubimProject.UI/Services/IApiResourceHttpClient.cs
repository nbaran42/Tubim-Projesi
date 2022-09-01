using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Identity.User;
using TubimProject.Application.Response;
using TubimProject.UI.Models;

namespace TubimProject.UI.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();

        Task<Response> Register(UserSaveModel userSaveViewModel);
        Task<Response> CreateRole(AddNewRole role);
        Task<Response> GetRoles();
        Task<Response> CreateUser(AddNewUser model);
        Task<Response> GetUsers();
        Task<Response> GetUserRoles(string userName);
        Task<Response> AddRoleToUserAsync(string userName,string roleName);
        Task<Response> RemoveRoleOfUser(string userName, string roleName);
        Task<Response> GetUserInformation(string userName);
    }
}

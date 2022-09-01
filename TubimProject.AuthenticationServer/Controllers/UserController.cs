using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TubimProject.AuthenticationServer.Models;
using static IdentityServer4.IdentityServerConstants;

namespace TubimProject.AuthenticationServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSaveModel model)
        {
            var kullanici = await userManager.CreateAsync(new ApplicationUser { UserName=model.UserName, Email="nbaran@egm.gov.tr" }, "Pp123456*");


            return Ok(kullanici.Succeeded);
        }
        public async Task<IActionResult> CreateRole(AddNewRole roleName)
        {
            var isRoleExists = await roleManager.RoleExistsAsync(roleName.RoleName);
            if (isRoleExists)
                return BadRequest($"{roleName.RoleName} is Already Exists in Database");

            await roleManager.CreateAsync(new IdentityRole
            {
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                Id=Guid.NewGuid().ToString(),
                Name=roleName.RoleName,
                NormalizedName=roleName.RoleName.Normalize(),
            });
            return Ok();

        }
        public IActionResult GetRoles() => Ok(roleManager.Roles.Select(r => new RolesViewModel { RoleId=r.Id, RoleName=r.Name }));

        public async Task<IActionResult> CreateUser(ApplicationUser model)
        {


            var user = await userManager.CreateAsync(model, "Pp123456");

            if (user.Succeeded)
            {
                return Ok();
            }
            return BadRequest(user.Errors.Select(r => r.Description).ToList());
        }
        public IActionResult GetUsers() => Ok(userManager.Users.Select(r => new ApplicationUser
        {
            Aktifmi=r.Aktifmi,
            CepTel=r.CepTel,
            Email=r.Email,
            EmailConfirmed=r.EmailConfirmed,
            KurumDaire=r.KurumDaire,
            PhoneNumber=r.PhoneNumber,
            T_Islem=r.T_Islem,
            Unvan=r.Unvan,
            UserName=r.UserName
        }));

        public async Task<IActionResult> GetUserRoles(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return Ok(await userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> AddRoleToUser(AddRoleToUser model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(r => r.Description).FirstOrDefault());
        }


        public async Task<IActionResult> RemoveRoleOfUser(AddRoleToUser model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(r => r.Description).FirstOrDefault());
        }

        public async Task<IActionResult> getUserInformation(string userName)
        {
            return Ok(await userManager.FindByNameAsync(userName));
        }
    }
}

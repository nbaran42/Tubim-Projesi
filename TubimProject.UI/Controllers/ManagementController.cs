using FluentValidation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.DTOs.Identity.User;
using TubimProject.Application.Features.KodTables.Command.Queries.GetKurumlar;
using TubimProject.UI.Services;

namespace TubimProject.UI.Controllers
{
    [Authorize]
    public class ManagementController : BaseController<ManagementController>
    {
        private readonly IValidator<AddNewUser> _userValidator;

        public ManagementController(IValidator<AddNewUser> userValidator)
        {
            _userValidator=userValidator;
        }

        #region Roles Management
        [Route("rol-yonetim-ekrani")]
        public IActionResult RolYonetim()
        {
            return View();
        }


        public async Task<IActionResult> GetRoles([DataSourceRequest] DataSourceRequest request)
        {
            var roles = await _apiResourceHttpClient.GetRoles();
            if (roles.isSuccess)
            {
                _logger.Warning("Rol Listesi {0} Tarafından Görüntülendi", User.UserName);
                var result = (List<RolesViewModel>)roles.Value;
                return new JsonResult(result.ToDataSourceResult(request));
            }
            return BadRequest(roles.Errors);
        }
        public async Task<IActionResult> getKurumList()
        {
            var kurumlar = await _mediator.Send(new GetKurumlarQuery());

            return Json(kurumlar.Data);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return BadRequest("Rol Adı Girmelisiniiz");
            var role = new AddNewRole { RoleName = roleName };
            var result = await _apiResourceHttpClient.CreateRole(role);
            if (result.isSuccess)
            {
                _logger.Warning("{0} İsimli Rol {1} Tarafından Eklendi.", roleName, User.UserName);
                return Ok();
            }
            return BadRequest(result.Errors);
        }
        #endregion

        #region User Management
        [Route("kullanici-yonetimi-islemleri")]
        public IActionResult UserManagement()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(AddNewUser model)
        {

            var validation = await _userValidator.ValidateAsync(model);


            if (validation.IsValid)
            {
                var result = await _apiResourceHttpClient.CreateUser(model);
                if (result.isSuccess)
                {
                    _logger.Warning("{0} İsimli Kullanıcı {1} Tarafından Eklendi.", model.UserName, User.UserName);
                    return Ok();
                }

                return BadRequest(result.Errors);
            }
            else
            {
                return BadRequest(validation.Errors?.Select(e => e.ErrorMessage).ToList());
            }

        }
        public async Task<IActionResult> GetUsers([DataSourceRequest] DataSourceRequest request)
        {

            var users = await _apiResourceHttpClient.GetUsers();
            if (users.isSuccess)
            {
                _logger.Warning("Sistemde Kayıtlı Tüm Kullanıcılar {0} Tarafından Görüntülendi.", User.UserName);
                var result = (List<AddNewUser>)users.Value;
                return new JsonResult(result.ToDataSourceResult(request));
            }
            return BadRequest(users.Errors);

        }
        #endregion

        #region User Roles Management

        public IActionResult UserRolesManagement()
        {
            return View();
        }

        public JsonResult GetUserRolesStatusItems()
        {
            List<DDPopulate> dDPopulates = new List<DDPopulate>();
            dDPopulates.Add(new DDPopulate { Id=1, Name=_stringLocalizer["userrols.ddListItem.addRemoveRole"] });
            dDPopulates.Add(new DDPopulate { Id=2, Name=_stringLocalizer["userrols.ddListItem.ResetPasswordLink"] });
            dDPopulates.Add(new DDPopulate { Id=3, Name=_stringLocalizer["userrols.ddListItem.HesapAski"] });
            dDPopulates.Add(new DDPopulate { Id=4, Name=_stringLocalizer["userrols.ddListItem.HesapAktiflestir"] });
            dDPopulates.Add(new DDPopulate { Id=5, Name=_stringLocalizer["userrols.ddListItem.EtkinlestirmeBaglantisiGonder"] });
            return Json(dDPopulates);
        }



        public async Task<ViewComponentResult> getEditUserRolesWindow(string username)
        {
            var userRoles = await _apiResourceHttpClient.GetUserRoles(username);

            return ViewComponent("UserRolesEditVC", new { username = username, roles = (IEnumerable<string>)userRoles.Value });
        }

        public async Task<IActionResult> GetAvailableRoles()
        {
            var result = await _apiResourceHttpClient.GetRoles();

            if (result.isSuccess)
            {
                return Json((List<RolesViewModel>)result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(string username, string roleName)
        {
            var result = await _apiResourceHttpClient.AddRoleToUserAsync(username, roleName);
            if (result.isSuccess)
            {
                _logger.Warning("{0} İsimli Rol {1} İsimli Kullanıcıya {2} Tarafından Atandı.", roleName, username, User.UserName);
                return Ok();
            }
            return BadRequest(result.Errors.FirstOrDefault());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleOfUser(string username, string roleName)
        {
            var result = await _apiResourceHttpClient.RemoveRoleOfUser(username, roleName);
            if (result.isSuccess)
            {
                _logger.Warning("{0} İsimli Rol {1} İsimli Kullanıcıdan {2} Tarafından Kaldırıldı.", roleName, username, User.UserName);
                return Ok();
            }
            return BadRequest(result.Errors.FirstOrDefault());

        }
        #endregion
    }
}

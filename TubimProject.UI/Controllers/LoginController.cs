using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TubimProject.UI.Services;
using TubimProject.Application.Validations.Authentication;
using TubimProject.Application.DTOs.Identity.Login;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace TubimProject.UI.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private readonly Serilog.ILogger _logger;
        IApiResourceHttpClient _apiResourceHttpClient;
        private readonly IStringLocalizer<LoginController> _localizer;

        public LoginController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient, IStringLocalizer<LoginController> localizer, Serilog.ILogger logger)
        {
            _configuration=configuration;
            _apiResourceHttpClient=apiResourceHttpClient;
            _localizer=localizer;
            _logger=logger;
        }

        public IActionResult Index(Uri ReturnUrl)
        {
            ViewData["message"]=_localizer.GetString("loginCaption");

            if (ReturnUrl is not null)
                return View(new LoginViewModel { ReturnUrl=ReturnUrl });

            if (ReturnUrl is null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(new LoginViewModel { ReturnUrl=ReturnUrl });
            }

            return View();
        }


       
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(LoginViewModel model)
        {

            //await _apiResourceHttpClient.CreateRole(new AddNewRole { RoleName="Test Role"});

            var loginValidator = new LoginValidator();
            var check = await loginValidator.ValidateAsync(model);

            if (check.IsValid)
            {
                var client = new HttpClient();
                var disco = await client.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);
                if (disco.IsError)
                {
                    _logger.Error("Yetkilendirme Sunucusuna Ulaşılamadı");
                    ModelState.AddModelError("", "Yetkilendirme Sunucusuna Ulaşılamadı!");
                    var errors = ModelState.Values.SelectMany(r => r.Errors).Select(r => r.ErrorMessage).ToArray();

                    return Json(new ReturnModel
                    {
                        Errors=errors,
                        isSuccess=false,
                    });
                }
                var password = new PasswordTokenRequest();


                password.Address=disco.TokenEndpoint;
                password.UserName=model.Username;
                password.Password=model.Password;
                password.ClientId=_configuration["ClientResourceOwner:ClientID"];
                password.ClientSecret=_configuration["ClientResourceOwner:ClientSecret"];

                var token = await client.RequestPasswordTokenAsync(password);
                if (token.IsError)
                {
                    _logger.Warning("Kullanıcı Adı/Şifre Hatalı");
                    ModelState.AddModelError("", "Kullanıcı Adı ya da Şifre Hatalı!");
                    var errors = ModelState.Values.SelectMany(r => r.Errors).Select(r => r.ErrorMessage).ToArray();
                    return Json(new ReturnModel
                    {
                        Errors=errors,
                        isSuccess=false,
                    });
                }


                var userinfoRequest = new UserInfoRequest();

                userinfoRequest.Token = token.AccessToken;
                userinfoRequest.Address = disco.UserInfoEndpoint;
                var userinfo = await client.GetUserInfoAsync(userinfoRequest);

                if (userinfo.IsError)
                {
                    _logger.Error("Kullanıcı Bilgisi Yetkilendirme Sunucusundan Alınırken Hata Oluştu");
                    ModelState.AddModelError("", "Kullanıcı Bilgisi Alınırken Hata Oluştu!");
                    var errors = ModelState.Values.SelectMany(r => r.Errors).Select(r => r.ErrorMessage).ToArray();
                    return Json(new ReturnModel
                    {
                        Errors=errors,
                        isSuccess=false,
                    });
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(userinfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authenticationProperties = new AuthenticationProperties();

                authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                      new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value= token.AccessToken},
                            new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value= token.RefreshToken},
                                  new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)}
            });

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
                _logger.Warning("{0} Kullanıcı Adı İle Sisteme Başarılı Bir Şekilde Giriş Yapıldı.",model.Username);
                return Json(new ReturnModel
                {

                    isSuccess=true,
                    ReturnUrl="main/anasayfa"
                });

            }
            else
            {
                _logger.Warning("Hatalı Giriş Denemesi Yapıldı.");
                var errors = check.Errors.Select(r => r.ErrorMessage).ToArray();
                return Json(new ReturnModel
                {
                    Errors=errors,
                    isSuccess=false, 
                });

            }


        }
    }
}

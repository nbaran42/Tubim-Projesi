using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TubimProject.Application.DTOs.Identity.User;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.UI.Services;

namespace TubimProject.UI.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IApiResourceHttpClient ApiResourceHttpClient;
        private UserInfo userInfo;
        private readonly Serilog.ILogger logger;
        private IStringLocalizer<T> stringLocalizer;
        private IMediator Mediator;
        private readonly IMapper Mapper;
        private ICacheService CacheService;
        protected IApiResourceHttpClient _apiResourceHttpClient => ApiResourceHttpClient??(HttpContext?.RequestServices.GetService<IApiResourceHttpClient>());
        protected IStringLocalizer<T> _stringLocalizer => stringLocalizer??(HttpContext?.RequestServices.GetService<IStringLocalizer<T>>());
        protected UserInfo User => (UserInfo)_apiResourceHttpClient.GetUserInformation(HttpContext?.User.Identity.Name).Result.Value;
        protected Serilog.ILogger _logger => logger??(HttpContext?.RequestServices.GetService<Serilog.ILogger>());
        protected IMediator _mediator => Mediator??(HttpContext?.RequestServices.GetService<IMediator>());
        protected IMapper _mapper => Mapper??(HttpContext?.RequestServices.GetService<IMapper>());

        protected ICacheService _cacheService =>CacheService??(HttpContext?.RequestServices.GetService<ICacheService>());
    }
}

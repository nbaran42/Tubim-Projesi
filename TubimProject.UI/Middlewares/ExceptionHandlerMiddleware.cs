using MediatR;
using TubimProject.Application.Features.ErrorLogs.Commands.Create;

namespace TubimProject.UI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
        private readonly Serilog.ILogger _logger;
        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, IHttpContextAccessor contextAccessor, IMediator mediator, Serilog.ILogger logger)
        {
            _requestDelegate=requestDelegate;
            _contextAccessor=contextAccessor;
            _mediator=mediator;
            _logger=logger;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception e)
            {

                _logger.Error("Global Hata Meydana Geldi.Hata Mesajı: {0}  ", e.Message);
                throw;
            }
        }


    }
}

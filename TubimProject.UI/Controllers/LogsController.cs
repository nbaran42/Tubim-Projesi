using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.Enums;
using TubimProject.Application.Features.CrashReport.Command.Create;
using TubimProject.Application.Features.CrashReport.Command.Update;
using TubimProject.Application.Features.CrashReport.Queries.GetAllCrashReports;
using TubimProject.Application.Features.CrashReport.Queries.GetCrashReportById;
using TubimProject.Application.Features.ErrorLogs.Queries.GetAllLogs;
using TubimProject.Application.Features.Notification.Commands.Create;

namespace TubimProject.UI.Controllers
{
    [Authorize]
    public class LogsController : BaseController<LogsController>
    {
        [Route("log-management/butun-loglar")]
        public IActionResult AllLogs()
        {
            return View();
        }

        [Route("hata-bildirimleri/butun-hata-bildirimleri")]
        public IActionResult HataBildirimleri()
        {
            return View();
        }

        public async Task<IActionResult> getAllLogs([DataSourceRequest] DataSourceRequest request)
        {
            var cacheData = _cacheService.GetData<List<GetAllLogsQueryResponse>>("alllogs");
            var result = await _mediator.Send(new GetAllLogsQuery());
            if (result.Succeeded)
            {
                _logger.Information("Log Listesi {0} Tarafından Görüntülendi", User.UserName);

                return new JsonResult(result.Data.ToDataSourceResult(request));
            }
            return BadRequest();
        }

        public async Task<IActionResult> getAllCrashReports([DataSourceRequest] DataSourceRequest request)
        {
            var result = await _mediator.Send(new GetAllCrashReportsQuery());
            if (result.Succeeded)
            {
                _logger.Information("Hata Bildirim Listesi {0} Tarafından Görüntülendi", User.UserName);

                return new JsonResult(result.Data.ToDataSourceResult(request));
            }
            return BadRequest();
        }
        public IActionResult getReportErrorWindow(string username)
        {
            return ViewComponent("winReportError", username);
        }
        public async Task<IActionResult> getCrashReportReplyWindow(int id)
        {
            var ss = new GetCrashReportByIdQuery(id);
            var result = await _mediator.Send(ss);
            if (result.Succeeded)
            {

                return ViewComponent("winCrashReportReply", result.Data);
            }
            else
            {

                return ViewComponent("winCrashReportReply");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCrashReport(ReportErrorAddModel model)
        {

            var crashReport = _mapper.Map<CreateCrashReportCommand>(model);
            var result = await _mediator.Send(crashReport);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyCrashReport(GetAllCrashReportsQueryResponse model)
        {
            var crashReport = new GetCrashReportByIdQuery(model.Id);
            var result = await _mediator.Send(crashReport);
            if (result.Succeeded)
            {
                var Reply = await _mediator.Send(new UpdateCrashReportCommand
                {
                    Id = model.Id,
                    ErrorDescription =result.Data.ErrorDescription +" Admin Cevabı: "+ model.ErrorDescription,
                    Username=model.Username,
                });


                if (Reply.Succeeded)
                {
                    var CreateNotification = await _mediator.Send(new CreateNotificationCommand
                    {
                        HasRead = false,
                        NotificationDescription=result.Data.T_Report +" Tarihli Yapmış Olduğunuz Hata İletimi Yönetici Tarafından Yanıtlanmıştır.",
                        NotificationType=(int)Enum_NotificationType.CRASH_REPORT_REPLIED,
                        ToUser=result.Data.Username,
                        T_Notification=DateTime.Now

                    });
                    if (CreateNotification.Succeeded)
                    {
                        _logger.Information("Hata Bildirimi {0} Tarafından Cevaplandı", User.UserName);
                        return Ok();
                    }

                    return BadRequest("Uyarı Servisi Veri Ekleyemedi.Tekrar Deneyin.");
                }
                return BadRequest("Hata İletisi Cevaplanırken Hata Oluştu.Tekrar Deneyin.");
            }
            return BadRequest("Cevaplanacak Hata İletisi Seçilemedi.Tekrar Deneyin");
        }


    }
}

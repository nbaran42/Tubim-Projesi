using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.DTOs.Logs;
using TubimProject.Application.Features.CrashReport.Command.Create;
using TubimProject.Application.Features.CrashReport.Command.Update;
using TubimProject.Application.Features.CrashReport.Queries.GetAllCrashReports;
using TubimProject.Application.Features.ErrorLogs.Commands.Create;
using TubimProject.Application.Features.ErrorLogs.Queries.GetAllLogs;
using TubimProject.Domain.Entities.Common;
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Application.Mappings
{
    public class ErrorLogProfile : Profile
    {
        public ErrorLogProfile()
        {
            CreateMap<CreateErrorLogCommand, ERRORLOGS>().ReverseMap();
            CreateMap<CreateCrashReportCommand, UT_CRASHREPORT>().ReverseMap();
            CreateMap<CreateCrashReportCommand, ReportErrorAddModel>().ReverseMap();
            CreateMap<UpdateCrashReportCommand, UT_CRASHREPORT>().ReverseMap();
            CreateMap<Logs, GetAllLogsQueryResponse>().ReverseMap();
            CreateMap<UT_CRASHREPORT, GetAllCrashReportsQueryResponse>().ReverseMap();
        }
    }
}

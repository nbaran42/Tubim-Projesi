using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.CrashReport.Queries.GetAllCrashReports;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;

namespace TubimProject.Application.Features.CrashReport.Queries.GetCrashReportById
{
    public class GetCrashReportByIdQuery : IRequest<Result<GetAllCrashReportsQueryResponse>>
    {
        public int Id { get; set; }
        public GetCrashReportByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
    public class GetCrashReportByIdQueryHandler : IRequestHandler<GetCrashReportByIdQuery, Result<GetAllCrashReportsQueryResponse>>
    {
        private ICrashReportService _crashReportService;
        private readonly IMapper _mapper;

        public GetCrashReportByIdQueryHandler(ICrashReportService crashReportService, IMapper mapper)
        {
            _crashReportService=crashReportService;
            _mapper=mapper;
        }

        public async Task<Result<GetAllCrashReportsQueryResponse>> Handle(GetCrashReportByIdQuery request, CancellationToken cancellationToken)
        {
            var crashReports = await _crashReportService.GetByIdAsync(request.Id);
            var result = _mapper.Map<GetAllCrashReportsQueryResponse>(crashReports);
            return Result<GetAllCrashReportsQueryResponse>.Success(result);
        }
    }

}

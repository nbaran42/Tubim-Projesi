using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;

namespace TubimProject.Application.Features.CrashReport.Queries.GetAllCrashReports
{
    public class GetAllCrashReportsQuery : IRequest<Result<List<GetAllCrashReportsQueryResponse>>>
    {
        public GetAllCrashReportsQuery()
        {

        }
    }
    public class GetAllCrashReportsQueryHandler : IRequestHandler<GetAllCrashReportsQuery, Result<List<GetAllCrashReportsQueryResponse>>>
    {
        private ICrashReportService _crashReportService;
        private readonly IMapper _mapper;

        public GetAllCrashReportsQueryHandler(ICrashReportService crashReportService, IMapper mapper)
        {
            _crashReportService=crashReportService;
            _mapper=mapper;
        }

        public async Task<Result<List<GetAllCrashReportsQueryResponse>>> Handle(GetAllCrashReportsQuery request, CancellationToken cancellationToken)
        {
            var crashReports = await _crashReportService.GetAllAsync(r => !r.LastModifiedOn.HasValue);
            var result = _mapper.Map<List<GetAllCrashReportsQueryResponse>>(crashReports);
            return Result<List<GetAllCrashReportsQueryResponse>>.Success(result);
        }
    }
}

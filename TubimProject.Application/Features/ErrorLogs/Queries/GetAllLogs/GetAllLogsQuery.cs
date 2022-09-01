using Abp.Domain.Uow;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Logs;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Domain.Entities.Logging;
namespace TubimProject.Application.Features.ErrorLogs.Queries.GetAllLogs
{
    public class GetAllLogsQuery : IRequest<Result<List<GetAllLogsQueryResponse>>>
    {
        public GetAllLogsQuery() {  }
    }
    public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery, Result<List<GetAllLogsQueryResponse>>>
    {
        private ISeriLogService _seriLogService;
        private readonly IMapper _mapper;

        public GetAllLogsQueryHandler(ISeriLogService seriLogService, IMapper mapper)
        {
            _seriLogService=seriLogService;
            _mapper=mapper;
        }



        public async Task<Result<List<GetAllLogsQueryResponse>>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {

            var logs = await _seriLogService.GetAllAsync();
            var result =_mapper.Map<List<GetAllLogsQueryResponse>>(logs);
            return   Result<List<GetAllLogsQueryResponse>>.Success(result);

        }
    }
}

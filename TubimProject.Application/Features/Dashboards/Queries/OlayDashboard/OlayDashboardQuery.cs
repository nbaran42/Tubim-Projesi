using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Cache;

namespace TubimProject.Application.Features.Dashboards.Queries.OlayDashboard
{
    public class OlayDashboardQuery : IRequest<Result<OlayDashboardQueryResponse>>
    {
        public OlayDashboardQuery()
        {

        }
    }
    public class OlayDashboardQueryHandler : IRequestHandler<OlayDashboardQuery, Result<OlayDashboardQueryResponse>>
    {
        private ICacheService _cacheService;
        private readonly IMapper _mapper;

        public OlayDashboardQueryHandler(ICacheService cacheService, IMapper mapper)
        {
            _cacheService=cacheService;
            _mapper=mapper;
        }

        public Task<Result<OlayDashboardQueryResponse>> Handle(OlayDashboardQuery request, CancellationToken cancellationToken)
        {

            var olayCacheData = _cacheService.GetData<OlayDashboardQueryResponse>("olaydashboard"); 
            return Task.FromResult(Result<OlayDashboardQueryResponse>.Success(data: olayCacheData));
        }
    }
}
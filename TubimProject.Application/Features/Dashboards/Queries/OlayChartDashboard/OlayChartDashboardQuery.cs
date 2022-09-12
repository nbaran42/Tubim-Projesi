using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Cache;

namespace TubimProject.Application.Features.Dashboards.Queries.OlayChartDashboard
{
    public class OlayChartDashboardQuery : IRequest<Result<List<OlayChartDashboardQueryResponse>>>
    {
        public OlayChartDashboardQuery()
        {

        }
    }

    public class OlayChartDashboardQueryHandler : IRequestHandler<OlayChartDashboardQuery, Result<List<OlayChartDashboardQueryResponse>>>
    {
        private ICacheService _cacheService;
        private readonly IMapper _mapper;

        public OlayChartDashboardQueryHandler(ICacheService cacheService, IMapper mapper)
        {
            _cacheService=cacheService;
            _mapper=mapper;
        }

        public Task<Result<List<OlayChartDashboardQueryResponse>>> Handle(OlayChartDashboardQuery request, CancellationToken cancellationToken)
        {
            var olayChartCacheData = _cacheService.GetData<List<OlayChartDashboardQueryResponse>>("olaychartdashboard");
            return Task.FromResult(Result<List<OlayChartDashboardQueryResponse>>.Success(data: olayChartCacheData));

        }
    }
}

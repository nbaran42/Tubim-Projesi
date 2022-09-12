using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Supheli.Queries.GetSonSupheliQuery;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Application.Interfaces.Repositories.Modules.SupheliModule;

namespace TubimProject.Application.Features.Supheli.Queries.GetAllSupheli
{
    public class GetAllSupheliQuery : IRequest<Result<List<GetSupheliQueryResponse>>>
    {
        public GetAllSupheliQuery()
        {

        }
    }
    public class GetAllSupheliQueryHandler : IRequestHandler<GetAllSupheliQuery, Result<List<GetSupheliQueryResponse>>>
    {
        private ISupheliService _supheliService;
        private readonly IMapper _mapper;

        public GetAllSupheliQueryHandler(ISupheliService supheliService, IMapper mapper)
        {
            _supheliService=supheliService;

            _mapper=mapper;
        }

        public async Task<Result<List<GetSupheliQueryResponse>>> Handle(GetAllSupheliQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _supheliService.GetAllAsync();
            var result = _mapper.Map<List<GetSupheliQueryResponse>>(gelenveri);
            return Result<List<GetSupheliQueryResponse>>.Success(result);
        }
    }
}

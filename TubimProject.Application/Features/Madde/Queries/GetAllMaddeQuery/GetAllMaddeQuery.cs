using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;

namespace TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery
{
    public class GetAllMaddeQuery : IRequest<Result<List<GetAllMaddeQueryResponse>>>
    {
        public GetAllMaddeQuery()
        {

        }
    }
    public class GetAllMaddeQueryHandler : IRequestHandler<GetAllMaddeQuery, Result<List<GetAllMaddeQueryResponse>>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;

        public GetAllMaddeQueryHandler(IMaddeService maddeService, IMapper mapper)
        {
            _maddeService=maddeService;
            _mapper=mapper;
        }

        public async Task<Result<List<GetAllMaddeQueryResponse>>> Handle(GetAllMaddeQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _maddeService.GetAllAsync();
            var result = _mapper.Map<List<GetAllMaddeQueryResponse>>(gelenveri);
            return Result<List<GetAllMaddeQueryResponse>>.Success(result);
        }
    }
}

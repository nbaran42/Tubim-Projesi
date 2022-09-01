using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Enums;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;

namespace TubimProject.Application.Features.Madde.Queries.GetSonMadde
{
    public class GetSonMaddeQuery : IRequest<Result<long?>>
    {
        public GetSonMaddeQuery()
        {

        }


    }

    public class GetSonMaddeQueryHandler : IRequestHandler<GetSonMaddeQuery, Result<long?>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;

        public GetSonMaddeQueryHandler(IMaddeService maddeService, IMapper mapper)
        {
            _maddeService=maddeService;
            _mapper=mapper;
        }

        public async Task<Result<long?>> Handle(GetSonMaddeQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _maddeService.GetAllAsync();
            var result = _mapper.Map<List<GetAllMaddeQueryResponse>>(gelenveri);
            return Result<long?>.Success(result.Max(r => r.JandarmaMaddeId));
        }
    }
}

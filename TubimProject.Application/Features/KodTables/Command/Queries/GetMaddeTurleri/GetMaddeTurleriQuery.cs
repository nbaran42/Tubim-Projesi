using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.Interfaces.Repositories.Modules.KurumlarKodTables;

namespace TubimProject.Application.Features.KodTables.Command.Queries.GetMaddeTurleri
{
    public class GetMaddeTurleriQuery : IRequest<Result<List<KodTablesResponse>>>
    {
        public GetMaddeTurleriQuery()
        {

        }
    }
    public class GetMaddeTurleriHandler : IRequestHandler<GetMaddeTurleriQuery, Result<List<KodTablesResponse>>>

    {

        IMaddeTurleriService _maddeTurleriService;
        private readonly IMapper _mapper;

        public GetMaddeTurleriHandler(IMaddeTurleriService maddeTurleriService, IMapper mapper)
        {
            _maddeTurleriService=maddeTurleriService;
            _mapper=mapper;
        }

        public async Task<Result<List<KodTablesResponse>>> Handle(GetMaddeTurleriQuery request, CancellationToken cancellationToken)
        {
            var maddeTurleri = await _maddeTurleriService.GetAllAsync();
            var result = _mapper.Map<List<KodTablesResponse>>(maddeTurleri);
            return Result<List<KodTablesResponse>>.Success(result);
        }
    }
}

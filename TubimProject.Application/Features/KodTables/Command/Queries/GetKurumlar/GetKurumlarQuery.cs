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

namespace TubimProject.Application.Features.KodTables.Command.Queries.GetKurumlar
{
    public class GetKurumlarQuery : IRequest<Result<List<KodTablesResponse>>>
    {
        public GetKurumlarQuery()
        {

        }
    }

    public class GetKurumlarQueryHandler : IRequestHandler<GetKurumlarQuery, Result<List<KodTablesResponse>>>
    {
        IKurumlarService _kurumlarService;
        private readonly IMapper _mapper;

        public GetKurumlarQueryHandler(IKurumlarService kurumlarService, IMapper mapper)
        {
            _kurumlarService=kurumlarService;
            _mapper=mapper;
        }

        public async Task<Result<List<KodTablesResponse>>> Handle(GetKurumlarQuery request, CancellationToken cancellationToken)
        {
            var kurumlar = await _kurumlarService.GetAllAsync();
            var result = _mapper.Map<List<KodTablesResponse>>(kurumlar);
            return Result<List<KodTablesResponse>>.Success(result);
        }
    }
}

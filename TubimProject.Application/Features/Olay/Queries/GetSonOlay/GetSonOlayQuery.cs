using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Enums;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;

namespace TubimProject.Application.Features.Olay.Queries.GetSonOlay
{
    public class GetSonOlayQuery : IRequest<Result<long?>>
    {
        public Enum_Kurumlar _Kurum { get; set; }
        public GetSonOlayQuery(Enum_Kurumlar Kurum)
        {
            _Kurum = Kurum;
        }
    }
    public class GetSonOlayQueryHandler : IRequestHandler<GetSonOlayQuery, Result<long?>>
    {
        private IOlayService _olayService;
        private IMapper _mapper;

        public GetSonOlayQueryHandler(IOlayService olayService, IMapper mapper)
        {
            _olayService=olayService;
            _mapper=mapper;
        }

        public async Task<Result<long?>> Handle(GetSonOlayQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _olayService.GetAllAsync(r => r.KurumRef==(int)request._Kurum);
            var result = _mapper.Map<List<GetAllOlaysQueryResponse>>(gelenveri);
            return Result<long?>.Success(result.Max(r => r.Id));
        }
    }
}

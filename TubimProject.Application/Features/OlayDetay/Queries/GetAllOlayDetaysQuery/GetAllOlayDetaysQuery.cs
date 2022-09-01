using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayDetayModule;

namespace TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery
{
    public class GetAllOlayDetaysQuery : IRequest<Result<List<GetAllOlayDetaysQueryResponse>>>
    {
        public GetAllOlayDetaysQuery()
        {

        }
    }
    public class GetAllOlayDetaysQueryHandler : IRequestHandler<GetAllOlayDetaysQuery, Result<List<GetAllOlayDetaysQueryResponse>>>
    {
        IOlayDetayService _olayDetayService;
        IMapper _mapper;

        public GetAllOlayDetaysQueryHandler(IOlayDetayService olayDetayService, IMapper mapper)
        {
            _olayDetayService=olayDetayService;
            _mapper=mapper;
        }

        public async Task<Result<List<GetAllOlayDetaysQueryResponse>>> Handle(GetAllOlayDetaysQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _olayDetayService.GetAllAsync();
            var result = _mapper.Map<List<GetAllOlayDetaysQueryResponse>>(gelenveri);
            return Result<List<GetAllOlayDetaysQueryResponse>>.Success(result);
        }
    }
}

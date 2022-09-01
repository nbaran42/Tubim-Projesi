using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;

namespace TubimProject.Application.Features.Olay.Queries.GetAllOlaylar
{
    public class GetAllOlaysQuery : IRequest<Result<List<GetAllOlaysQueryResponse>>>
    {
        public GetAllOlaysQuery()
        {

        }
    }

    public class GetAllOlaysQueryHandler : IRequestHandler<GetAllOlaysQuery, Result<List<GetAllOlaysQueryResponse>>>
    {
        private IOlayService _olayService;
        private IMapper _mapper;

        public GetAllOlaysQueryHandler(IOlayService olayService, IMapper mapper)
        {
            _olayService=olayService;
            _mapper=mapper;
        }

        public async Task<Result<List<GetAllOlaysQueryResponse>>> Handle(GetAllOlaysQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _olayService.GetAllAsync();
            var result = _mapper.Map<List<GetAllOlaysQueryResponse>>(gelenveri); 
            return Result<List<GetAllOlaysQueryResponse>>.Success(result);
        }
    }

}

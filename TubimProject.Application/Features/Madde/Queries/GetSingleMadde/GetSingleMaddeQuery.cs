using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;

namespace TubimProject.Application.Features.Madde.Queries.GetSingleMadde
{
    public class GetSingleMaddeQuery : IRequest<Result<GetAllMaddeQueryResponse>>
    {
        public GetSingleMaddeQuery(long olayNo, string malzemeNo)
        {
            OlayNo=olayNo;
            MalzemeNo=malzemeNo;
        }

        public long OlayNo { get; set; }
        public string MalzemeNo { get; set; }
    }
    public class GetSingleMaddeQueryHandler : IRequestHandler<GetSingleMaddeQuery, Result<GetAllMaddeQueryResponse>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;

        public GetSingleMaddeQueryHandler(IMaddeService maddeService, IMapper mapper)
        {
            _maddeService=maddeService;
            _mapper=mapper;
        }

        public async Task<Result<GetAllMaddeQueryResponse>> Handle(GetSingleMaddeQuery request, CancellationToken cancellationToken)
        {
            var gelenveri = await _maddeService.FirstOrDefaultAsync(r => r.OlayNumarasi==request.OlayNo && r.MalzemeNo==request.MalzemeNo, new CancellationToken());
            var result = _mapper.Map<GetAllMaddeQueryResponse>(gelenveri);
            return Result<GetAllMaddeQueryResponse>.Success(result);
        }
    }
}

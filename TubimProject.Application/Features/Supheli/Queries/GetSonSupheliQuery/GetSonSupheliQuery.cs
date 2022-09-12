using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Enums;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Application.Interfaces.Repositories.Modules.SupheliModule;

namespace TubimProject.Application.Features.Supheli.Queries.GetSonSupheliQuery
{
    public class GetSonSupheliQuery : IRequest<Result<int?>>
    {
        public GetSonSupheliQuery()
        {

        }
    }
    public class GetSonSupheliQueryHandler : IRequestHandler<GetSonSupheliQuery, Result<int?>>
    {
        private ISupheliService _supheliService;
        private IOlayService _olayService;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public GetSonSupheliQueryHandler(ISupheliService supheliService, IOlayService olayService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _supheliService=supheliService;
            _olayService=olayService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int?>> Handle(GetSonSupheliQuery request, CancellationToken cancellationToken)
        {
            var olay = await _olayService.GetAllAsync(r => r.KurumRef==(int)Enum_Kurumlar.JANDARMA); 
            var supheliler = await _supheliService.GetAllAsync();

            var result = (from a in olay join b in supheliler on a.KurumOlayId equals b.OlayId orderby b.SupheliId descending select b.SupheliId).FirstOrDefault();
             
            return Result<int?>.Success(result);
        }
    }
}

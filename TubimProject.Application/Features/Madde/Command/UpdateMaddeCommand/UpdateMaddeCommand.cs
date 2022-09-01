
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Application.Features.Madde.Command.UpdateMaddeCommand
{
    public class UpdateMaddeCommand : IRequest<Result<int>>
    {
        public UpdateMaddeCommand(GetAllMaddeQueryResponse madde,string MaddeAdi)
        {
            _madde = madde;
            _MaddeAdi=MaddeAdi;
        }

        public GetAllMaddeQueryResponse _madde { get; set; }
        public string _MaddeAdi { get; set; }
    }
    public class UpdateMaddeCommandHandler : IRequestHandler<UpdateMaddeCommand, Result<int>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UpdateMaddeCommandHandler(IMaddeService maddeService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _maddeService=maddeService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateMaddeCommand request, CancellationToken cancellationToken)
        {
            var guncellenecekMadde = await _maddeService.FirstOrDefaultAsync(r => r.OlayNumarasi==request._madde.OlayNumarasi && r.MalzemeNo==request._madde.MalzemeNo, cancellationToken);


            guncellenecekMadde.JandarmaMaddeId=request._madde.JandarmaMaddeId;
            guncellenecekMadde.AnaTuru=request._madde.AnaTuru;
            guncellenecekMadde.Birimi=request._madde.Birimi;
            guncellenecekMadde.Miktari=request._madde.Miktari;
            guncellenecekMadde.MalzemeNo=request._madde.MalzemeNo;
            guncellenecekMadde.OlayNumarasi=request._madde.OlayNumarasi;
            guncellenecekMadde.Durum="D";
            guncellenecekMadde.ServisAnaTuru=request._MaddeAdi;
            var result = _mapper.Map<UT_MADDE>(guncellenecekMadde);
            _maddeService.Update(result);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success();
        }
    }
}

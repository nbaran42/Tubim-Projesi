using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;

namespace TubimProject.Application.Features.Madde.Command.CreateMaddeCommand
{
    public class CreateMaddeCommand : IRequest<Result<int>>
    {
        public long OlayId { get; set; }
        public long? JandarmaMaddeId { get; set; }
        public string? EgmMaddeId { get; set; }
        public long OlayNumarasi { get; set; }
        public string? AnaTuru { get; set; }
        public string? AltTuru { get; set; }
        public string? Birimi { get; set; }
        public decimal? Miktari { get; set; }
        public string? MalzemeNo { get; set; }
        public string? KaynakUlke { get; set; }
        public string? Durum { get; set; }
        public string? ServisAnaTuru { get; set; }

    }
    public class CreateMaddeCommandHandler : IRequestHandler<CreateMaddeCommand, Result<int>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public CreateMaddeCommandHandler(IMaddeService maddeService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _maddeService=maddeService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateMaddeCommand request, CancellationToken cancellationToken)
        {


            await _maddeService.InsertAsync(new Domain.Entities.Tpds.UT_MADDE
            {
                AltTuru=request.AltTuru,
                AnaTuru=request.AnaTuru,
                Birimi=request.Birimi,
                Durum=request.Durum,
                JandarmaMaddeId=request.JandarmaMaddeId,
                KaynakUlke=request.KaynakUlke,
                MalzemeNo=request.MalzemeNo,
                Miktari=request.Miktari,
                OlayId=request.OlayId,
                OlayNumarasi=request.OlayNumarasi,
                ServisAnaTuru = request.ServisAnaTuru
            });
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success();
        }
    }
}

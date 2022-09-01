 
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayDetayModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Application.Features.OlayDetay.Command
{
    public class CreateOlayDetayKayitCommand : IRequest<Result<int>>
    {
        public long? OlayId { get; set; }
        public string? OlayBeldeKoy { get; set; }
        public string? OlayIlcesi { get; set; }
        public string? OlayIli { get; set; }
        public string? OlayMahalle { get; set; }
        public string? OlayEnlem { get; set; }
        public string? OlayBoylam { get; set; }
        public string? OlayBolgesi { get; set; }

    }
    public class CreateOlayDetayKayitCommandHandler : IRequestHandler<CreateOlayDetayKayitCommand, Result<int>>
    {
        private IOlayDetayService _olayService;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public CreateOlayDetayKayitCommandHandler(IOlayDetayService olayService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _olayService=olayService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateOlayDetayKayitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var olayKayit = _mapper.Map<UT_OLAYDETAY>(request);
                await _olayService.InsertAsync(olayKayit);
                return await Result<int>.SuccessAsync();
            }
            catch (Exception ex)
            {
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}

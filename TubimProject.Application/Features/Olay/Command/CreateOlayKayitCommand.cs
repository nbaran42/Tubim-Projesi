 
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Application.Features.Olay.Command
{
    public class CreateOlayKayitCommand : IRequest<Result<int>>
    {
        public long? KurumOlayId { get; set; }
        public int KurumRef { get; set; }
        public string? HedefUlkesi { get; set; }
        public string? KacakcilikYontemi { get; set; }
        public string? MudahaleEdenBirim { get; set; }
        public long? OlayNumarasi { get; set; }
        public DateTime? OlayTarihi { get; set; }
        public string? SistemNo { get; set; }
        public string? Durum { get; set; }
    }

    public class CreateOlayKayitCommandHandler : IRequestHandler<CreateOlayKayitCommand, Result<int>>
    {
        private IOlayService _olayService;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public CreateOlayKayitCommandHandler(IOlayService olayService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _olayService=olayService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateOlayKayitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var olayKayit = _mapper.Map<UT_OLAY>(request);
                await _olayService.InsertAsync(olayKayit);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync();
            }
            catch (Exception ex)
            {
                return await Result<int>.FailAsync(ex.Message);
            }

        }
    }
}

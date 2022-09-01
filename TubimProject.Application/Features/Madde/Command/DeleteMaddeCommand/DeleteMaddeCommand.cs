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

namespace TubimProject.Application.Features.Madde.Command.DeleteMadde
{
    public class DeleteMaddeCommand : IRequest<Result<int>>
    {
        public GetAllMaddeQueryResponse _madde { get; set; }


        public DeleteMaddeCommand(GetAllMaddeQueryResponse madde)
        {
            _madde=madde;
        }
    }

    public class DeleteMaddeCommandHandler : IRequestHandler<DeleteMaddeCommand, Result<int>>
    {
        private IMaddeService _maddeService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public DeleteMaddeCommandHandler(IMaddeService maddeService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _maddeService=maddeService;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteMaddeCommand request, CancellationToken cancellationToken)
        {
            var silinecekMAdde = await _maddeService.FirstOrDefaultAsync(r => r.JandarmaMaddeId==request._madde.JandarmaMaddeId, cancellationToken);
            var result = _mapper.Map<UT_MADDE>(silinecekMAdde);
            _maddeService.Delete(result);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success();
        }
    }
}

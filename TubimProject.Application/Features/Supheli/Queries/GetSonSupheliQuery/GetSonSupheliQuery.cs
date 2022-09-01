using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.SupheliModule;

namespace TubimProject.Application.Features.Supheli.Queries.GetSonSupheliQuery
{
    public class GetSonSupheliQuery : IRequest<Result<GetSupheliQueryResponse>>
    {
        public GetSonSupheliQuery()
        {

        }
    }
    public class GetSonSupheliQueryHandler : IRequestHandler<GetSonSupheliQuery, Result<GetSupheliQueryResponse>>
    {
        private ISupheliService _supheliService;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public Task<Result<GetSupheliQueryResponse>> Handle(GetSonSupheliQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

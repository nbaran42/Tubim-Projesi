using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Supheli.Queries.GetSonSupheliQuery;

namespace TubimProject.Application.Features.Supheli.Queries.GetSingleSupheli
{
    public class GetSingleSupheliQuery : IRequest<Result<GetSupheliQueryResponse>>
    {
        public GetSingleSupheliQuery(long olayNo, int supheliNo)
        {
            _OlayNo=olayNo;
            _SupheliNo=supheliNo;
        }

        public long _OlayNo { get; set; }
        public int _SupheliNo { get; set; }
    }
    public class GetSingleSupheliQueryHandler : IRequestHandler<GetSingleSupheliQuery, Result<GetSupheliQueryResponse>>
    {
        public Task<Result<GetSupheliQueryResponse>> Handle(GetSingleSupheliQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

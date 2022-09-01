using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories; 
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Application.Features.ErrorLogs.Commands.Create
{
    public class CreateErrorLogCommand : IRequest<Result<int>>
    {
        public string USERNAME { get; set; }
        public string USERIP { get; set; }
        public DateTime T_ERROR { get; set; }
        public string ERRORPATH { get; set; }
        public string ERRORDESCRIPTION { get; set; }




        public class CreateErrorLogCommandHandler : IRequestHandler<CreateErrorLogCommand, Result<int>>
        {
           
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork;
            
            public CreateErrorLogCommandHandler( IMapper mapper, IUnitOfWork unitOfWork)
            {
                 
                _mapper=mapper;
                _unitOfWork=unitOfWork;
            }

            public async Task<Result<int>> Handle(CreateErrorLogCommand request, CancellationToken cancellationToken)
            {
                var errorLog = _mapper.Map<ERRORLOGS>(request);
                
                try
                {
                    
                    await _unitOfWork.Commit(cancellationToken);
                }
                catch (Exception ex) 
                {

                    throw;
                }
          
                return await Result<int>.SuccessAsync();
            }
        }
    }


}

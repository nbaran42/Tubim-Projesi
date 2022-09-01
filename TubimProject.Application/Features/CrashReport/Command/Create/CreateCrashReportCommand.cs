
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Application.Validations.Common;
using TubimProject.Domain.Entities.Common;

namespace TubimProject.Application.Features.CrashReport.Command.Create
{
    public class CreateCrashReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime T_Report => DateTime.Now;
        public string ErrorDescription { get; set; }

        public class CreateCrashReportCommandHandler : IRequestHandler<CreateCrashReportCommand, Result<int>>
        {
            private ICrashReportService _crashReport;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork;

            public CreateCrashReportCommandHandler(ICrashReportService crashReport, IMapper mapper, IUnitOfWork unitOfWork)
            {
                _crashReport=crashReport;
                _mapper=mapper;
                _unitOfWork=unitOfWork;
            }

            public async Task<Result<int>> Handle(CreateCrashReportCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var validasyon = new SendCrashReportValidator();
                    var isValid = await validasyon.ValidateAsync(request);
                    if (isValid.IsValid)
                    {
                        var crashReport = _mapper.Map<UT_CRASHREPORT>(request);
                        await _crashReport.InsertAsync(crashReport);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync();

                    }
                    var errors = isValid.Errors.Select(r => r.ErrorMessage).ToList();
                    return await Result<int>.FailAsync(String.Join(",", errors));

                }
                catch (Exception e)
                {
                    return await Result<int>.FailAsync(e.Message);
                }

            }
        }
    }
}

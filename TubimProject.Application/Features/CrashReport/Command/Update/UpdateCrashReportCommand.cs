using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Domain.Entities.Common;

namespace TubimProject.Application.Features.CrashReport.Command.Update
{
    public class UpdateCrashReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime T_Report => DateTime.Now;
        public string ErrorDescription { get; set; }



        public class UpdateCrashReportCommandHandler : IRequestHandler<UpdateCrashReportCommand, Result<int>>
        {
            private ICrashReportService _crashReportService;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork;

            public UpdateCrashReportCommandHandler(ICrashReportService crashReportService, IMapper mapper, IUnitOfWork unitOfWork)
            {
                _crashReportService=crashReportService;
                _mapper=mapper;
                _unitOfWork=unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCrashReportCommand request, CancellationToken cancellationToken)
            {
                var guncellenecekKayit = _mapper.Map<UT_CRASHREPORT>(request);

                if (guncellenecekKayit!=null)
                {
                    var dbData = await _crashReportService.GetByIdAsync(guncellenecekKayit.Id);
                    _crashReportService.Update(dbData);
                    return Result<int>.Success();
                }
                return Result<int>.Fail("Guncellenecek Kayit Bulunamadi");

            }
        }
    }
}

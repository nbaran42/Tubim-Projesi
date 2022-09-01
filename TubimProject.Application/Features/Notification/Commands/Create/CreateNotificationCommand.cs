using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.Notification;
using TubimProject.Domain.Entities.Common;

namespace TubimProject.Application.Features.Notification.Commands.Create
{
    public class CreateNotificationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int NotificationType { get; set; }
        public DateTime T_Notification { get; set; }
        public string NotificationDescription { get; set; }
        public string ToUser { get; set; }
        public bool HasRead { get; set; }



        public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result<int>>
        {
            private INotificationService _notificationService;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork;

            public CreateNotificationCommandHandler(INotificationService notificationService, IMapper mapper, IUnitOfWork unitOfWork)
            {
                _notificationService=notificationService;
                _mapper=mapper;
                _unitOfWork=unitOfWork;
            }

            public async Task<Result<int>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var notification = _mapper.Map<UT_NOTIFICATION>(request);
                    await _notificationService.InsertAsync(notification);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync();
                }
                catch (Exception e)
                {

                    return await Result<int>.FailAsync(e.Message);
                }
            }
        }
    }
}

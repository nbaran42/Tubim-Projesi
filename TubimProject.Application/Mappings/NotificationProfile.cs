using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TubimProject.Application.Features.Notification.Commands.Create;
using TubimProject.Domain.Entities.Common;
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Application.Mappings
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<CreateNotificationCommand, UT_NOTIFICATION>().ReverseMap();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.Notification;
using TubimProject.Domain.Entities.Common;

namespace TubimProject.Infrastructure.Repositories.Modules.NotificationModule
{
    public class NotificationService:BaseGenericRepo<UT_NOTIFICATION>,INotificationService
    {
        public NotificationService(ApplicationDbContext context) : base(context)
        { }
    }
}

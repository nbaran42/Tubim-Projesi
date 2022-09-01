using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Notification
{
    public class AddNotification
    {
        public int Id { get; set; }
        public int NotificationType { get; set; }
        public DateTime T_Notification { get; set; }
        public string NotificationDescription { get; set; }
    }
}

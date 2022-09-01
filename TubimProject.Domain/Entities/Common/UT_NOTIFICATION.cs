using Abp.Auditing;
using AspNetCoreHero.Abstractions.Domain;

namespace TubimProject.Domain.Entities.Common
{
    [Audited]
    public class UT_NOTIFICATION : AuditableEntity
    {
        public int Id { get; set; }
        public int NotificationType { get; set; }
        public DateTime T_Notification { get; set; }
        public string NotificationDescription { get; set; }
        public string ToUser { get; set; }
        public bool HasRead { get; set; }
    }
}
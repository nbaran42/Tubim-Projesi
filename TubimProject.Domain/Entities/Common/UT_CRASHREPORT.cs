using Abp.Auditing;
using AspNetCoreHero.Abstractions.Domain;

namespace TubimProject.Domain.Entities.Common
{
    [Audited]
    public class UT_CRASHREPORT : AuditableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime T_Report { get; set; }
        public string ErrorDescription { get; set; }
    }
}
using Abp.Auditing;
using AspNetCoreHero.Abstractions.Domain;

namespace TubimProject.Domain.Entities.Logging
{
    [Audited]
    public class ERRORLOGS : AuditableEntity
    {
        public string USERNAME { get; set; }
        public string USERIP { get; set; }
        public DateTime? T_ERROR { get; set; }
        public string ERRORPATH { get; set; }
        public string ERRORDESCRIPTION { get; set; }
    }
}
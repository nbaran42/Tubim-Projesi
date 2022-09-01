using Abp.Auditing;
using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

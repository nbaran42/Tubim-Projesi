using Abp.Auditing;
using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Domain.Entities.Tpds
{
    [Audited]
    public class UT_SUCTANIM : AuditableEntity
    {
        public long OlayId { get; set; }
        public string? SucTanimi { get; set; }
        public string? UzunSucTanimi { get; set; }
        [ForeignKey("OlayId")]
        public virtual UT_OLAY? UT_OLAY { get; set; }
    }
}

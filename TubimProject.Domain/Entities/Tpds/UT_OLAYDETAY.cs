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
    public class UT_OLAYDETAY : AuditableEntity
    {
        public long? OlayId { get; set; }
        public string? OlayBeldeKoy { get; set; }
        public string? OlayIlcesi { get; set; }
        public string? OlayIli { get; set; }
        public string? OlayMahalle { get; set; }
        public string? OlayEnlem { get; set; }
        public string? OlayBoylam { get; set; }
        public string? OlayBolgesi { get; set; }
        [ForeignKey("OlayId")]
        public virtual UT_OLAY? UT_OLAY { get; set; }
    }
}

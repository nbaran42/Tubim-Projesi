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
    public class UT_MADDE : AuditableEntity
    {
        public long OlayId { get; set; }
        public long? JandarmaMaddeId { get; set; }
        public string? EgmMaddeId { get; set; }
        public long OlayNumarasi { get; set; }
        public string? AnaTuru { get; set; }
        public string? AltTuru { get; set; }
        public string? Birimi { get; set; }
        public decimal? Miktari { get; set; }
        public string? MalzemeNo { get; set; }
        public string? KaynakUlke { get; set; }
        public string? Durum { get; set; }
        public string? ServisAnaTuru { get; set; }
        [ForeignKey("OlayId")]
        public virtual UT_OLAY? UT_OLAY { get; set; }

    }
}

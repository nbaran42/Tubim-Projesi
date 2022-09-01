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
    public class UT_OLAY : AuditableEntity
    {
        [ForeignKey("Id")]
        public long Id { get; set; }
        public long? KurumOlayId { get; set; }
        public int KurumRef { get; set; }
        public string? HedefUlkesi { get; set; }
        public string? KacakcilikYontemi { get; set; }
        public string? MudahaleEdenBirim { get; set; }
        public long? OlayNumarasi { get; set; }
        public DateTime? OlayTarihi { get; set; }
        public string? SistemNo { get; set; }
        public string? Durum { get; set; }
        public virtual List<UT_OLAYDETAY>? UT_OLAYDETAY { get; set; }
        public virtual List<UT_MADDE>? UT_MADDE { get; set; }
        public virtual List<UT_SUCTANIM>? UT_SUCTANIM { get; set; }
    }
}

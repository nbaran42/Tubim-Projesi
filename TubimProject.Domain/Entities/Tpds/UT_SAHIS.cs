﻿using Abp.Auditing;
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
    public class UT_SAHIS : AuditableEntity
    {
        [ForeignKey("Id")]
        public long Id { get; set; }
        public int OlayId { get; set; }
        public int? SupheliId { get; set; }
        public long OlayNumarasi { get; set; }
        public long? SupheliNo { get; set; }
        public string? Cinsiyet { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? MedeniDurumu { get; set; }
        public string? Meslek { get; set; }
        public string? TCKimlikNoPasaportNo { get; set; }
        public string? Uyrugu { get; set; }
        public string? Durum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Domain.Entities.Logging
{
    public class UT_AUDIT
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime AuditDate { get; set; }
     
    }
}

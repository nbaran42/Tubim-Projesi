using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Common
{
    public class CrashReportModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime T_Report { get; set; }
        public string ErrorDescription { get; set; }

    }
}

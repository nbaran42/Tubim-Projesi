using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Common
{
    public class ReportErrorAddModel
    {
        public string Username { get; set; }
        private DateTime T_Report => DateTime.Now;
        public string ErrorDescription { get; set; }
    }
}

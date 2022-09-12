using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Features.Dashboards.Queries.MaddeChartDashboard.Queries
{
    public class MaddeChartDashboardQueryResponse
    {
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }
        public string MassType { get; set; }
        public string MassName { get; set; }
    }
}

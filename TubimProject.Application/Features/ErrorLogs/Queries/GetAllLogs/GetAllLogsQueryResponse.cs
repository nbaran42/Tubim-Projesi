﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Features.ErrorLogs.Queries.GetAllLogs
{
    public class GetAllLogsQueryResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
    }
}
 

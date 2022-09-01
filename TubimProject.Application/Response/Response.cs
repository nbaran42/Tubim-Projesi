using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Response
{
    public class Response
    {
        
        public bool isSuccess { get; set; }
        public string Errors { get; set; }
        public object? Value { get; set; } 
    }
}

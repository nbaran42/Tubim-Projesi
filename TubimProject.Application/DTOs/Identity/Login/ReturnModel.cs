using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Identity.Login
{
    public class ReturnModel
    { 
        public bool isSuccess { get; set; }
        public string[] Errors { get; set; }
        public string ReturnUrl { get; set; }
    }
}

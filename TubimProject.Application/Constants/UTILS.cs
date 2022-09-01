using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Constants
{
    public static class UTILS
    {
        public static string AUTH_SERVER_BASE_URL = "https://localhost:5001/api/";
        public static Uri? JBaseUrl { get; set; } = new Uri("https://10.200.0.71/VeriPaylasimServis.API");
    }
}

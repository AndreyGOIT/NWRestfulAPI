using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWRestfulAPI.Models
{
    public class AppSettings
    {
        public string JwtSecret { get; set; } = string.Empty;
    }
}
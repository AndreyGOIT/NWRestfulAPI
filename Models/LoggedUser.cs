using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWRestfulAPI.Models
{
    public class LoggedUser
    {
        public string Username { get; set; } = string.Empty;
        public int AccesslevelId { get; set; }
        public string? Token { get; set; } = string.Empty;
    }
}
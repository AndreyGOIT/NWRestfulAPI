using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Services.Interfaces
{
    public interface IAuthenticateService
    {
        LoggedUser? Authenticate(string username, string password);
    }
}
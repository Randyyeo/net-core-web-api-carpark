using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace carpark
{
    public interface iJWTAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}

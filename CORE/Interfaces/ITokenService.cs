using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities.Identity;

namespace CORE.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
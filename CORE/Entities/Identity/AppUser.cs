using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CORE.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Displayname { get; set; }
        public Address Address { get; set; }

        
    }
}
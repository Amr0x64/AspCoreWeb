using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }  
        

    }
    
}

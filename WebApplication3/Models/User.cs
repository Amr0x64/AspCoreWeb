using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }  
    }
    
}

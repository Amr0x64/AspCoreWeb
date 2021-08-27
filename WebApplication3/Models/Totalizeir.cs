using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Totalizeir
    {
        public Totalizeir(IRepository repo)
        {
            Repository = repo;
        }
        public IRepository Repository { get; set; }
        
    }
}

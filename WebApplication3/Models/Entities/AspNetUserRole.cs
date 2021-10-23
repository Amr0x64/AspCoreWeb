using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication3
{
    public partial class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        
        /*
        [ForeignKey("UserId")]
        public AspNetUser AspNetUser { get; set; }
        
        [ForeignKey("RoleId")]
        public AspNetRole AspNetRole { get; set; }*/
    }
}

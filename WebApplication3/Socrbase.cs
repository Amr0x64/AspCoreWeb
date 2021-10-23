using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication3
{
    public partial class Socrbase
    {
        [Key]
        public int? Level { get; set; }
        public string Socrname { get; set; }
        public string Scname { get; set; }
        public string KodTSt { get; set; }
    }
}

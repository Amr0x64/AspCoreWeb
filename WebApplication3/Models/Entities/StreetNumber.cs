using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models.Entities
{
    public class StreetNumber
    {
        [Key]
        public Guid StreetNumberID {get; set;}
        public Guid FiasGuid { get; set; }
        public string HouseNumber { get; set; }
        public int? StrucNumber { get; set; }
        public int? Counter { get; set; }
        public string FlatNumber { get; set; }
        public int FlatType { get; set; }
        public string PostalCode { get; set; }
        public Guid? PrevId { get; set; }
        public Guid? NextId { get; set; }
        public int LiveStatus { get; set; }
        public string AddUser { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? EndDate { get; set; }

        public FiasStatment FiasStatment { get; set; }
    }
}

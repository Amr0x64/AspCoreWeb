using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class FiasStatment
    {
        [Key]
        public Guid FiasStatmentId { get; set; }
        public int ActStatus { get; set; }
        public Guid FiasGuid { get; set; }
        public int Level { get; set; }
        public int CurrStatus { get; set; }
        [MaxLength(120)]
        public string AdressName { get; set; }
        public Guid? NextId { get; set; }
        public Guid ParentId { get; set; }
        public Guid? PrevId { get; set; }
        public string ShortTypeName { get; set; }
        public string TypeName { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string AddUser { get; set; }

    }
}

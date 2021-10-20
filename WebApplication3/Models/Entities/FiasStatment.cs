using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication3
{
    public partial class FiasStatment
    {
        public int? ActStatus { get; set; }
        public Guid? FiasGuid { get; set; }
        [Key]
        public Guid? FiasStatementsId { get; set; }
        public int? Level { get; set; }
        public int? CurrStatus { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(120)]
        public string AddressName { get; set; }
        public Guid? NextId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? PrevId { get; set; }
        public string ShortTypeName { get; set; }
        public string TypeName { get; set; }
        public DateTime? StartDate { get; set; }
        public string AddUser { get; set; }
    }
}

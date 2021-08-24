using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3
{
    public partial class FiasView
    {
        public Guid? FiasStatementsId { get; set; }
        public int? ActStatus { get; set; }
        public Guid? FiasGuid { get; set; }
        public int? Level { get; set; }
        public int? CurrStatus { get; set; }
        public string AddressName { get; set; }
        public Guid? NextId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? PrevId { get; set; }
        public string ShortTypeName { get; set; }
        public string TypeName { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string AddUser { get; set; }
    }
}

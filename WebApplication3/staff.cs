using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3
{
    public partial class staff
    {
        public Guid StaffId { get; set; }
        public string FurstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string AddUser { get; set; }
        public DateTime AddDate { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool IsRemoved { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class FiasStatment
    {
        [Key]
        public Guid fias_statements_id { get; set; }
        public int act_status { get; set; }
        public Guid fias_guid { get; set; }
        public int level { get; set; }
        public int curr_status { get; set; }
        [MaxLength(120)]
        public string address_name { get; set; }
        public Guid? next_id { get; set; }
        public Guid? parent_id { get; set; }
        public Guid? prev_id { get; set; }
        public string short_type_name { get; set; }
        public string type_name { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? start_date { get; set; }
        public string add_user { get; set; }

        public List<StreetNumber> StreetNumbers { get; set; }
    }
}

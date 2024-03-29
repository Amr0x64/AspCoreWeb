﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication3.Models.Entities
{
    public partial class StreetNumber
    {
        [Key]
        public Guid? StreetNumberID { get; set; }
        
        public Guid FiasGuid { get; set; }
        
        public string HouseNumber { get; set; }
        
        public string StrucNumber { get; set; }
        
        public int? Counter { get; set; }
        
        public string FlatNumber { get; set; }
        
        public int? FlatType { get; set; }
        
        public string PostalCode { get; set; }
        
        public Guid? PrevId { get; set; }
        
        public Guid? NextId { get; set; }
        
        public int? LiveStatus { get; set; }
        
        public string AddUser { get; set; }
        
        public DateTime? Startdate { get; set; }
            
        public DateTime? EndDate { get; set; }
        
        [ForeignKey("StreetNumberID")]
        public FiasStatment FiasStatment { get; set; }
    }
}

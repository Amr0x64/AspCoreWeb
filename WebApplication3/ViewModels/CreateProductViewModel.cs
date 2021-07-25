using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication3.ViewModels
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Введите название товара")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Введите положительное значение для цены")]
        public int Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Введите положительное значение для количества")]
        public int Count { get; set; }
        public string AddUser { get; set; }
        public DateTime? AddDate { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }

    }
}

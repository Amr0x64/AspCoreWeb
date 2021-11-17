using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.ViewModels;
using WebApplication3.Models;

namespace WebApplication3.Components
{
    public class ProductCategories : ViewComponent
    {
        private RPRCContext _db;

        public ProductCategories(RPRCContext context)
        {
            _db = context;
        }
        public IViewComponentResult Invoke()
        {
            ProductCategoriesViewModel model = new ProductCategoriesViewModel();
            model.Categories = _db.Products.Select(p => p.Category).Distinct().ToList();
            
            return View("~/Views/Home/Components/ProductCategories/Default.cshtml", model);
        }
    }
}   
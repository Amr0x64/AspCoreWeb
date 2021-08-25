using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication3.Controllers
{
    public class OrderRestController : Controller
    {
        private ApplicationContext db;
        private IConfiguration Configuration;
        public OrderRestController(ApplicationContext context, IConfiguration configuration)
        {
            db = context;
            Configuration = configuration;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM FiasStatments", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<FiasStatment> _adressList = new List<FiasStatment>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    Guid guid = new Guid("11111111-1111-1111-1111-111111111111");
                    FiasStatment adress = new FiasStatment
                    {
                        ActStatus = (int)cells[0],
                        FiasGuid = (Guid)cells[1],
                        FiasStatmentId = (Guid)cells[2],
                        Level = (int)cells[3],
                        AddressName = (string)cells[6]
                    };

                    if (!(cells[8] != null)) adress.ParentId = (Guid)cells[8];
                    else adress.ParentId = guid;

                    _adressList.Add(adress);    
                }
            }
            //ds.WriteXml("products.xml");
            connection.Close();
            string address = HttpContext.Request.Query["Adress"].ToString();
            var modelAdress = _adressList.Where(l => l.Level != 9 && l.Level == 8).OrderByDescending(l => l.Level).Select(a => a.AddressName).ToList();
            return Ok(modelAdress);
        }
    }
}

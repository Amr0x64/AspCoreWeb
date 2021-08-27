using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApplication3.ApiController
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;
        public ReservationController(IRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return repository.Reservations;
        }
        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return repository[id];
        }
        [HttpPost]
        public Reservation Post([FromBody] Reservation res)
        {
            return repository.AddReservation(new Reservation { ClientName = res.ClientName, Location = res.Location });
        }
        [HttpPut]
        public Reservation Put([FromBody] Reservation res)
        {
            return repository.UpdateReservation(res);
        }
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.DeleteReservation(id);
        }
    }  
}

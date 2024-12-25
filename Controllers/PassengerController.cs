using AIW3_DewaPermana_SMKN8JEMBER.DTO;
using AIW3_DewaPermana_SMKN8JEMBER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIW3_DewaPermana_SMKN8JEMBER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PassengerController(EsemkaRailwaysContext context) : ControllerBase
    {
        EsemkaRailwaysContext _context = context;

        [HttpGet]
        public ActionResult GetAll ()
        {
            return Ok(_context.Passengers.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult GetById (int id)
        {
            var idUsr = _context.Passengers.Find(id);
            if (idUsr == null)
            {
                return NotFound( new { message = "Passenger Not Found!"});
            } 
            var tiket = _context.Tickets.Where( t => t.PassengerId == id).Select( usr => new { usr.TicketId, usr.SeatNumber, usr.ScheduleId, usr.BookingTime});
            return Ok(new
            {
                Passenger = idUsr,
                Ticket = tiket
            });
        }

        [HttpPost]
        public ActionResult PostPassenger(passengerDTO passengerDto)
        {
            Passenger passenger = new Passenger()
            {
                FirstName = passengerDto.FirstName,
                LastName = passengerDto.LastName,
                Email = passengerDto.Email,
                PhoneNumber = passengerDto.Phone
            };

            var existing_email = _context.Passengers.FirstOrDefault( usr => usr.Email == passengerDto.Email );
            var existing_phone = _context.Passengers.FirstOrDefault( usr => usr.PhoneNumber == passengerDto.Phone );
            if (existing_email != null) return BadRequest(new { message = "Email Already exists!" }) ; 
            else if (existing_phone != null) return BadRequest( new { message = "Phone Number Already exist!"});
            else if (passengerDto.Phone.Length > 15) return BadRequest(new { message = "The length Phone Number cannot Melebihi 15 hehe, Maap gbisa Inggris"});
            else {
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
                return CreatedAtAction(nameof(PostPassenger), new { id = passenger.PassengerId}, passenger);
            }
        }

        [HttpPut("{id}")]
        public ActionResult putPass(int id, passengerDTO passengerDTO)
        {
            Passenger pas = _context.Passengers.Find(id);
            if (pas == null) return NotFound(new { message = "Id Passenger Not Found" });
            pas.PhoneNumber = passengerDTO.Phone;
            pas.Email = passengerDTO.Email;
            pas.FirstName = passengerDTO.FirstName;
           if (passengerDTO.Phone.Length > 15) return BadRequest(new { message = "Phone Number Don't Melebihi 15 hehe, Maap mas gabisa Bahasa Inggris" });
            _context.Passengers.Update(pas);
            _context.SaveChanges();
            return Ok(pas);
        }

        [HttpDelete("{id}")]
        public ActionResult deletePass(int id)
        {
            Passenger pass = _context.Passengers.Find(id);
            if (pass == null) return NotFound(new { message = "Passenger Not Found" });
            _context.Passengers.Remove(pass);
            _context.SaveChanges();
            return Ok(new {message = "Succesfully  Delete Passenger Data!"});
        }
    }
}

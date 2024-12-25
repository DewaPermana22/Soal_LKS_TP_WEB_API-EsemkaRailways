using AIW3_DewaPermana_SMKN8JEMBER.DTO;
using AIW3_DewaPermana_SMKN8JEMBER.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AIW3_DewaPermana_SMKN8JEMBER.Models.ModelsRespon;

namespace AIW3_DewaPermana_SMKN8JEMBER.Controllers
{
    [Route("api/Schedules")]
    [ApiController]
    public class TicketController(EsemkaRailwaysContext context) : ControllerBase
    {
        EsemkaRailwaysContext _context = context;
        [HttpGet("{PassengerID}")]
        public IActionResult Get(int PassengerID, [FromQuery] int? trainID, [FromQuery] DateTime? departureDate)
        {
            var ticket = _context.Tickets.Where( pas => pas.PassengerId == PassengerID );
            if (trainID.HasValue)
            {
                ticket = ticket.Where(t => t.Schedule.TrainId == trainID.Value);
            }
            if (departureDate.HasValue)
            {
                ticket = ticket.Where(dt => dt.Schedule.DepartureTime == departureDate.Value);
            }
            var tickets = ticket.Include(t => t.Schedule)
                              .Include(t => t.Schedule.Train)
                              .Include(t => t.Schedule.DepartureStation)
                              .Include(t => t.Schedule.ArrivalStation)
                              .ToList();

            if (tickets.Count == 0)
            {
                return NotFound(new { message = "Tiket Not Found based on kriteria yang diberikan. (Maap mas gapati iso bahasa inggris hehe)" });
            }

            var res_tiket = tickets.Select(t => new
            {
                t.TicketId,
                t.PassengerId,
                t.SeatNumber,
                t.ScheduleId,
                t.BookingTime
            }).ToList();

            var ticketResponses = tickets.Select(t => new ModelsRespon.TicketResponse
            {
                TicketId = t.TicketId,
                PassengerId = (int)t.PassengerId,
                ScheduleId = (int)t.ScheduleId,
                SeatNumber = t.SeatNumber,
                BookingTime = (DateTime)t.BookingTime
            }).ToList();

            var schedule = tickets.FirstOrDefault()?.Schedule;
            var scheduleResponse = new ModelsRespon.ScheduleResponse
            {
                ScheduleId = schedule?.ScheduleId ?? 0,
                TrainId = schedule?.TrainId ?? 0,
                DepartureStation = schedule?.DepartureStation?.StationName,
                ArrivalStation = schedule?.ArrivalStation?.StationName,
                DepartureTime = schedule?.DepartureTime ?? DateTime.MinValue,
                ArrivalTime = schedule?.ArrivalTime ?? DateTime.MinValue
            };

            var train = schedule?.Train;
            var trainResponse = new ModelsRespon.TrainResponse
            {
                TrainId = train?.TrainId ?? 0,
                TrainName = train?.TrainName,
                Capacity = train?.Capacity ?? 0,
                Type = train?.Type
            };

            var response = new ModelsRespon.TicketDetailsResponse
            {
                Tickets = ticketResponses,
                Schedule = scheduleResponse,
                Train = trainResponse
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult posTicket([FromBody] ticketsDTO ticketsDTO)
        {
            var pass = _context.Passengers.FirstOrDefault( pas => pas.PassengerId == ticketsDTO.PassengerID );
            if (pass == null) return NotFound(new { message = "Passenger Not found!"});
            var scheID = _context.Schedules.FirstOrDefault(sch => sch.ScheduleId == ticketsDTO.ScheduleID);
            if (scheID == null) return NotFound(new { message = "Schedule Not found!"});

            var ticket = new List<Ticket>();
            foreach (var seat_num in ticketsDTO.SeatNumber) {
                var tiket = new Ticket()
                {
                    PassengerId = ticketsDTO.PassengerID,
                    ScheduleId = ticketsDTO.ScheduleID,
                    SeatNumber = seat_num,
                    BookingTime = DateTime.Now
                };
                ticket.Add(tiket);
            }
            _context.Tickets.AddRange(ticket);
            _context.SaveChanges();

            return CreatedAtAction(nameof(posTicket), new { passengerId = ticketsDTO.PassengerID }, ticket);
        }
    }
}

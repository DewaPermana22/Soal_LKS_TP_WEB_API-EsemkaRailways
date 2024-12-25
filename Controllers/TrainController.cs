using AIW3_DewaPermana_SMKN8JEMBER.DTO;
using AIW3_DewaPermana_SMKN8JEMBER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIW3_DewaPermana_SMKN8JEMBER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController(EsemkaRailwaysContext context) : ControllerBase
    {
        EsemkaRailwaysContext _context = context;

        [HttpGet]
        public IActionResult GetAllTrain() {
            return Ok(_context.Trains.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetTrainByID(int id) {
            var trainID = _context.Trains.Find(id);
            if (trainID == null)
            {
                return NotFound(new { message = "Train Not Found!" });
            }
            var schedule = _context.Schedules.Where(t => t.TrainId == id)
                .Select(usr => new {
                    usr.ScheduleId,
                    usr.DepartureTime,
                    DepartureStationName = usr.DepartureStation.StationName,
                    usr.ArrivalTime,
                    ArrivalStationName = usr.ArrivalStation.StationName
                }).ToList();
            return Ok(new
            {
                Train = trainID,
                schedule
            });
        }
        [HttpPost]
        public IActionResult PostTrain(TrainDTO trainDTO) {
            Train train = new Train
            {
                TrainName = trainDTO.TrainName,
                Capacity = trainDTO.Capacity,
                Type = trainDTO.Type,
            };
            if (trainDTO.Type != "Express" && trainDTO.Type != "Local") return BadRequest(new { message = "Train type must be Local or Express" });
            else if (trainDTO.Capacity <= 0) return BadRequest(new { message = "Capacity Must be More than 0" });
            else
            {
                _context.Trains.Add(train);
                _context.SaveChanges();
                return CreatedAtAction(nameof(PostTrain), new { id = train.TrainId }, train);
            }
        }
        [HttpPut("{id}")]
        public IActionResult PutTrain(int id, TrainDTO trainDTO)
        {
            Train train = _context.Trains.Find(id);
            if (train == null) return NotFound(new { message = "Train Not found!" });
            train.TrainName = trainDTO.TrainName;
            train.Capacity = trainDTO.Capacity;
            train.Type = trainDTO.Type;
            if (trainDTO.Type != "Express" && trainDTO.Type != "Local") return BadRequest(new { message = "Train type must be Local or Express" });
            else
            {
                _context.Trains.Update(train);
                _context.SaveChanges();
                return Ok(train);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrain(int id)
        {
            Train train = _context.Trains.Find(id);
            if (train == null) return NotFound(new { message = "Train Not found!"});
            _context.Trains.Remove(train);
            _context.SaveChanges();
            return Ok(new { message = "Succesfully Delete Train Data!" });
        }
    }
}

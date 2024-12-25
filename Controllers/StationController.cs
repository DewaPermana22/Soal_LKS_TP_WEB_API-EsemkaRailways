using AIW3_DewaPermana_SMKN8JEMBER.DTO;
using AIW3_DewaPermana_SMKN8JEMBER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIW3_DewaPermana_SMKN8JEMBER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController(EsemkaRailwaysContext context) : ControllerBase
    {
        EsemkaRailwaysContext _context = context;
        [HttpGet]
        public IActionResult getAll() {
            return Ok(_context.Stations.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult getByID(int id) {
            Station station = _context.Stations.Find(id);
            if (station == null) return NotFound(new { message = "Station Not Found!" });
            var scheduleTime = _context.Schedules.Where( st => st.ArrivalStationId == id || st.DepartureStationId == id)
                .Select( st => new
                {
                    st.ScheduleId,
                    st.Train.TrainName,
                    st.DepartureTime,
                    st.ArrivalTime
                }).ToList();
            return Ok( new
            {
                Station = station,
                ScheduleInfo = scheduleTime
            });
        }

        [HttpPost]
        public IActionResult post(stationDTO stationDTO) {
            Station station = new Station()
            {
                StationName = stationDTO.stationName,
                Location = stationDTO.Location
            };
            _context.Stations.Add(station);
            _context.SaveChanges();
            return CreatedAtAction(nameof(post), new { id = station.StationId }, station);
        }
        [HttpPut("{id}")]
        public IActionResult put(int id, stationDTO stationDTO)
        {
            Station st = _context.Stations.Find(id);
            if (st == null) return NotFound(new { message = "Station Not found!" });
            st.StationName = stationDTO.stationName;
            st.Location = stationDTO.Location;
            _context.Stations.Update(st);
            _context.SaveChanges();
            return Ok(st);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            Station station = _context.Stations.Find(id);
            if (station == null) return NotFound(new { message = "Station Not Found!" });
            _context.Stations.Remove(station);
            _context.SaveChanges();
            return Ok(new {message = "Succesfully Delete Station Data!"});
        }
    }
}

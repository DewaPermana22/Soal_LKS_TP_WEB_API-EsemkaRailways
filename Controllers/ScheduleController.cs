using AIW3_DewaPermana_SMKN8JEMBER.DTO;
using AIW3_DewaPermana_SMKN8JEMBER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace AIW3_DewaPermana_SMKN8JEMBER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(EsemkaRailwaysContext context) : ControllerBase
    {
        EsemkaRailwaysContext _context = context;

        [HttpGet]
        public IActionResult getSchedule()
        {
            var details = _context.Schedules.Select(det => new
            {
                det.ScheduleId,
                det.TrainId,
                TrainName = det.Train.TrainName,
                det.DepartureTime,
                det.DepartureStationId,
                DepartureStationName = det.DepartureStation.StationName,
                det.ArrivalTime,
                det.ArrivalStationId,
                ArrivalStationName = det.ArrivalStation.StationName,
                
            });
            return Ok(details);
        }

        [HttpPost]
        public IActionResult postSchedule(scheduleDTO scheduleDTO)
        {
            var train = _context.Trains.FirstOrDefault(t => t.TrainId == scheduleDTO.TrainID);
            var departureStation = _context.Stations.FirstOrDefault(s => s.StationId == scheduleDTO.depStationID);
            var arrivalStation = _context.Stations.FirstOrDefault(s => s.StationId == scheduleDTO.arvStationID);

            Schedule schedule = new Schedule()
            {
                TrainId = scheduleDTO.TrainID,
                DepartureStationId = scheduleDTO.depStationID,
                ArrivalStationId = scheduleDTO.arvStationID,
                DepartureTime = scheduleDTO.DepTime,
                ArrivalTime = scheduleDTO.ArvTime,
            };

            if (scheduleDTO.DepTime >= scheduleDTO.ArvTime) return BadRequest(new { message = "Departure Time must Before ArivalTime!" });
            if (scheduleDTO.DepTime.Date != scheduleDTO.ArvTime.Date) return BadRequest(new { message = "Departure and Arival must be on the same day!" });
            if (scheduleDTO.arvStationID == null || scheduleDTO.depStationID == null || scheduleDTO.TrainID == null)
            {
                return BadRequest(new { message = "Station or Train not found." });
            }

            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return CreatedAtAction(nameof(postSchedule), new { 
                id = schedule.ScheduleId,
                TrainName = train.TrainName,
                DepartureStationName = departureStation.StationName,
                ArivalStationName = arrivalStation.StationName,
            }, schedule);
        }

        [HttpPut("{id}")]
        public IActionResult putSchedule(int id, scheduleDTO scheduleDTO)
        {
            var train = _context.Trains.FirstOrDefault(t => t.TrainId == scheduleDTO.TrainID);
            var departureStation = _context.Stations.FirstOrDefault(s => s.StationId == scheduleDTO.depStationID);
            var arrivalStation = _context.Stations.FirstOrDefault(s => s.StationId == scheduleDTO.arvStationID);

            Schedule schedule = _context.Schedules.Find(id);
            if (schedule == null) return BadRequest(new { message = "Schedule Npt found!" });
            schedule.TrainId = scheduleDTO.TrainID;
            schedule.DepartureTime = scheduleDTO.DepTime;
            schedule.ArrivalTime = scheduleDTO.ArvTime;
            schedule.DepartureStationId = scheduleDTO.depStationID;
            schedule.ArrivalStationId = scheduleDTO.arvStationID;
            if (scheduleDTO.DepTime >= scheduleDTO.ArvTime) return BadRequest(new { message = "Departure Time must Before ArivalTime!" });
            if (scheduleDTO.DepTime.Date != scheduleDTO.ArvTime.Date) return BadRequest(new { message = "Departure and Arival must be on the same day!" });
            _context.Schedules.Update(schedule);
            _context.SaveChanges();
            return Ok(new
            {
                id = schedule.ScheduleId,
                TrainName = train.TrainName,
                DepartureStationName = departureStation.StationName,
                ArivalStationName = arrivalStation.StationName,
                schedule
            });
        }
        [HttpDelete("{id}")]
        public IActionResult deleteSchedule(int id) {
            Schedule schedule = _context.Schedules.Find(id);
            if (schedule == null) return BadRequest(new { message = "Schedule Not Found!" });
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
            return Ok( new
            {
                message = "Succesfully Delete Schedule Data!"
            });
        }
    }
}

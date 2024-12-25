using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class Station
{
    public int StationId { get; set; }

    [Required]
    public string? StationName { get; set; }
    [Required]
    public string? Location { get; set; }
    [JsonIgnore]
    public virtual ICollection<Schedule> ScheduleArrivalStations { get; set; } = new List<Schedule>();
    [JsonIgnore]
    public virtual ICollection<Schedule> ScheduleDepartureStations { get; set; } = new List<Schedule>();
}

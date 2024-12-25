using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? TrainId { get; set; }

    public int? DepartureStationId { get; set; }

    public int? ArrivalStationId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public virtual Station? ArrivalStation { get; set; }

    public virtual Station? DepartureStation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Train? Train { get; set; }
}

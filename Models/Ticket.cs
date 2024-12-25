using System;
using System.Collections.Generic;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? PassengerId { get; set; }

    public int? ScheduleId { get; set; }

    public string? SeatNumber { get; set; }

    public DateTime? BookingTime { get; set; }

    public virtual Passenger? Passenger { get; set; }

    public virtual Schedule? Schedule { get; set; }
}

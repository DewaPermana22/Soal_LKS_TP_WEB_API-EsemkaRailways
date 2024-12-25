namespace AIW3_DewaPermana_SMKN8JEMBER.Models
{
    public class ModelsRespon
    {
        public class TicketResponse
        {
            public int TicketId { get; set; }
            public int PassengerId { get; set; }
            public int ScheduleId { get; set; }
            public string SeatNumber { get; set; }
            public DateTime BookingTime { get; set; }
        }

        public class ScheduleResponse
        {
            public int ScheduleId { get; set; }
            public int TrainId { get; set; }
            public string DepartureStation { get; set; }
            public string ArrivalStation { get; set; }
            public DateTime DepartureTime { get; set; }
            public DateTime ArrivalTime { get; set; }
        }

        public class TrainResponse
        {
            public int TrainId { get; set; }
            public string TrainName { get; set; }
            public int Capacity { get; set; }
            public string Type { get; set; }
        }

        public class TicketDetailsResponse
        {
            public List<TicketResponse> Tickets { get; set; }
            public ScheduleResponse Schedule { get; set; }
            public TrainResponse Train { get; set; }
        }

    }
}

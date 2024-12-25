using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class Train
{
    public int TrainId { get; set; }
    [Required]
    public string? TrainName { get; set; }
    [Required]
    public int? Capacity { get; set; }
    [Required]
    public string? Type { get; set; }

    [JsonIgnore]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}

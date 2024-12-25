using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class Passenger
{
    [Required]
    public int PassengerId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

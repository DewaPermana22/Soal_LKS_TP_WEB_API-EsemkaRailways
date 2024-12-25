using System.ComponentModel.DataAnnotations;

namespace AIW3_DewaPermana_SMKN8JEMBER.DTO
{
    public class passengerDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}

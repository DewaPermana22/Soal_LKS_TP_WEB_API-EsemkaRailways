using System.ComponentModel.DataAnnotations;

namespace AIW3_DewaPermana_SMKN8JEMBER.DTO
{
    public class TrainDTO
    {
        [Required]
        public string TrainName { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Type { get; set; }
    }
}

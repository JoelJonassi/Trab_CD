using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models
{
    public class Time
    {
        [Key]
        public int IdTime { get; set; }
        public int time { get; set; }
    }
}

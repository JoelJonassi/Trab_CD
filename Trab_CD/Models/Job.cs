using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models
{
    public class Job
    {
        [Key]
        public int IdJob { get; set; }
        public string NameJob { get; set; }
        public List<Time> times { get; set; }
        public List<Operation> Operations { get; set; }
    
    }
}

namespace JobShopAPI.Models
{
    public class JobOperation
    {
        
        public int IdJob { get; set; }
        public Job Job { get; set; }
        public int IdOperation { get; set; } 
        public Operation Operation { get; set; }
    }
}

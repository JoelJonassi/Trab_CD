namespace JobShopAPI.Models
{
    public class Job
    {
        public int IdJob { get; set; }
        public string NameJob { get; set; }
        public Operation Operations { get; set; }
    
    }
}

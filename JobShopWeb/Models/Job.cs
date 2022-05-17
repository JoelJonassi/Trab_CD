namespace JobShopWeb.Models
{
    public class Job
    {
        public int IdJob { get; set; }
        public string NameJob { get; set; }
        public int? IdOperation { get; set; }
        public Operation Operation { get; set; }

    }
}

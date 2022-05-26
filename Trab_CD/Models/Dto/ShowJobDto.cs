namespace JobShopAPI.Models.Dto
{
    public class ShowJobDto
    {
        public int IdJob { get; set; }
        public string NameJob { get; set; }
        public int IdOperation { get; set; }
        public string NameOperation { get; set; }   
        public string NameMachine { get; set; }
        public int IdMachine { get; set; }
        public int Time { get; set; }
    }
}

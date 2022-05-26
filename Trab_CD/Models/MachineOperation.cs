namespace JobShopAPI.Models
{
    public class MachineOperation
    {
        public int IdMachine { get; set; }
        public Machine Machine { get; set; }
        public int IdOperation { get; set; }
        public Operation Operation { get; set; }
    }
}

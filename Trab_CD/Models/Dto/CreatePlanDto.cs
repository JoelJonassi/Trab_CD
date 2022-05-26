namespace JobShopAPI.Models
{
    public class CreatePlanDto
    {
        public int IdSimulation { get; set; }

        public int IdJob { get; set; }

        public int IdOperation { get; set; }

        public int IdMachine { get; set; }

        public int initialTime { get; set; }

        public int finalTime { get; set; }
    }
}

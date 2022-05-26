namespace JobShopAPI.Models
{
    public class JobSimulation
    {
        public int IdJob { get; set; }
        public Job Job { get; set; }
        public int IdSimulation { get; set; }
        public Simulation Simulation { get; set; }

    }
}

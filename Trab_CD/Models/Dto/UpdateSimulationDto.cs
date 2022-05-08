namespace JobShopAPI.Models.Dto
{
    public class UpdateSimulationDto {
        public int IdSimulation { get; set; }
        public List<Machine> Machines { get; set; }
        public List<Operation> Operations { get; set; }
        public int IdJob { get; set; }
    }
}

namespace JobShopAPI.Models.Dto
{
    public class CreateSimulationDto {

        public int IdSimulation { get; set; }
        public Machine Machine { get; set; }
        public Operation Operation { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }

}

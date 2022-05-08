﻿namespace JobShopAPI.Models.Dto
{
    public class SimulationDto
    {
        public int IdSimulation { get; set; }
        public Machine Machine { get; set; }
        public Operation Operation { get; set; }
        public List<Job> Jobs { get; set; }
    }
}

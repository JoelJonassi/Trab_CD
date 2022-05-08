using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;

namespace JobShopAPI.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Simulation, CreateSimulationDto>().ReverseMap();
            CreateMap<Simulation, UpdateSimulationDto>().ReverseMap();
            CreateMap<Simulation, SimulationDto>().ReverseMap();
           
        }
       
    }
}

using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;

namespace JobShopAPI.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Simulation, CreateSimulationDto>().ReverseMap();
            CreateMap<Simulation, UpdateSimulationDto>().ReverseMap();
            CreateMap<Simulation, SimulationDto>().ReverseMap();

            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Job, CreateJobDto>().ReverseMap();
            CreateMap<Job, UpdateJobDto>().ReverseMap();

            CreateMap<Operation, OperationDto>().ReverseMap();
            CreateMap<Operation, CreateOperationDto>().ReverseMap();
            CreateMap<Operation, UpdateOperationDto>().ReverseMap();

            CreateMap<Machine, CreateMachineDto>().ReverseMap();
            CreateMap<Machine, MachineDto>().ReverseMap();
            CreateMap<Machine, UpdateMachineDto>().ReverseMap();

            CreateMap<User,CreateUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, ChangePasswordUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


        }
       
    }
}

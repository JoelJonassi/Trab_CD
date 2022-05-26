using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IMachineRepository
    {
        bool MachineExists(string NameSimulation);
        bool MachineExists(int IdSimulation);
        ICollection<Machine> GetMachines();
        Machine GetMachine(int IdMachine);
        bool CreateMachine(Machine machine);
        bool UpdateMachine(Machine machine);
        bool DeleteMachine(Machine machine);
        bool Save();
    }
}

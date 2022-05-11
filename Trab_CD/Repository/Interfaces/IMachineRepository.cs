using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IMachineRepository
    {
        bool MachineExists(string NameSimulation);
        bool MachineExists(int IdSimulation);
        ICollection<Machine> GetMachines();
        Machine GetMachine(int IdSimulation);
        bool CreateMachine(Machine simulation);
        bool UpdateMachine(Machine simulation);
        bool DeleteMachine(Machine simulation);
        bool Save();
    }
}

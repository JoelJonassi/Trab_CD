using JobShopAPI.Models;
using JobShopWeb.Repository.IRepository;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IMachineOperationRepository
    {
        bool Exists(int Id);
        ICollection<MachineOperation> GetAll();
        MachineOperation Get(int Id);
        bool Create(MachineOperation objects);
        bool Update(MachineOperation objects);
        bool Delete(MachineOperation objects);
        bool Save();
    }
}

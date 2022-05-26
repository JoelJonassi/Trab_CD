using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IPlanRepository
    {
        bool Exists(int idPlan);
        bool Exists(int IdSimulation, int IdJob, int IdMachine, int IdOperation);
        ICollection<Plan> GetAll();
        Plan Get(int Id);
        bool Create(Plan objects);
        bool Update(Plan objects);
        bool Delete(Plan objects);
        bool Save();
    }
}

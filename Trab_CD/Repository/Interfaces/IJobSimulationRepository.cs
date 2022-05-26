using JobShopAPI.Models;
using JobShopWeb.Repository.IRepository;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IJobSimulationRepository
    {
        bool Exists(int Id);
        ICollection<JobSimulation> GetAll();
        JobSimulation Get(int Id);
        bool Create(JobSimulation objects);
        bool Update(JobSimulation objects);
        bool Delete(JobSimulation objects);
        bool Save();

    }
}

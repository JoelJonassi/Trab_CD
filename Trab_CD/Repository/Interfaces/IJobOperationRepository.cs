using JobShopAPI.Models;
using JobShopWeb.Repository.IRepository;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IJobOperationRepository
    {
        bool Exists(int Id);
        ICollection<JobOperation> GetAll();
        JobOperation Get(int Id);
        bool Create(JobOperation objects);
        bool Update(JobOperation objects);
        bool Delete(JobOperation objects);
        bool Save();

    }
}


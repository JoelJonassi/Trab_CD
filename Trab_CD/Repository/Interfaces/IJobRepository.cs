using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IJobRepository
    {
        bool JobExists(string NameSimulation);
        bool JobExists(int IdSimulation);
        ICollection<Job> GetJobs();
        Job GetJob(int IdSimulation);
        bool CreateJob(Job simulation);
        bool UpdateJob(Job simulation);
        bool DeleteJob(Job simulation);
        bool Save();
    }
}

using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IJobModel
    {
        //Serviço de tabela de produção
        bool AddOperationInJob(Job job, Operation operation);
        bool DeleteOperationInJob(Job job, Operation operation);
        bool UpdateOperationInJob(Job job, Operation operation);
        bool DeleteJob(Job job);
    }
}

using JobShopAPI.Models;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IOperationRepository
    {
        bool OperationExists(string NameOperation);
        bool OperationExists(int IdOperation);
        ICollection<Operation> GetOperations();
        Operation GetOperation(int IdOperation);
        bool CreateOperation(Operation operation);
        bool UpdateOperation(Operation operation);
        bool DeleteOperation(Operation operation);
        bool Save();
    }
}

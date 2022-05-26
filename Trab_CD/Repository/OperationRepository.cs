using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.Interfaces;

namespace JobShopAPI.Repository
{
    public class OperationRepository: IOperationRepository
    {
        //Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public OperationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CreateOperation(Operation simulation)
        {
            _db.Operations.Add(simulation);
            return Save();
        }


        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool OperationExists(string NameSimulation)
        {
            return _db.Operations.Any(simu => simu.OperationName == NameSimulation);
        }

        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool OperationExists(int IdSimulation)
        {
            return _db.Operations.Any(simu => simu.IdOperation == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<Operation> GetOperations()
        {
            return _db.Operations.OrderBy(simu => simu.IdOperation).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public Operation GetOperation(int IdSimulation)
        {
            return _db.Operations.FirstOrDefault(simu => simu.IdOperation == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateOperation(Operation simulation)
        {
            _db.Operations.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteOperation(Operation operation)
        {
            _db.Operations.Remove(operation);
            return Save();
        }

        /// <summary>
        /// Função que guarda uma simulação
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}

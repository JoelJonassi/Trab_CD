using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace JobShopAPI.Repository.Interfaces
{
    public class MachineRepository: IMachineRepository
    {
        //Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public MachineRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CreateMachine(Machine simulation)
        {
            _db.Machines.Add(simulation);
            return Save();
        }


        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool MachineExists(string NameSimulation)
        {
            return _db.Machines.Any(simu => simu.MachineName == NameSimulation);
        }

        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool MachineExists(int IdSimulation)
        {
            return _db.Machines.Any(simu => simu.IdMachine == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<Machine> GetMachines()
        {
            return _db.Machines.OrderBy(simu => simu.IdMachine).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public Machine GetMachine(int IdSimulation)
        {
            return _db.Machines.FirstOrDefault(simu => simu.IdMachine == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateMachine(Machine simulation)
        {
            _db.Machines.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteMachine(Machine simulation)
        {
            _db.Machines.Remove(simulation);
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

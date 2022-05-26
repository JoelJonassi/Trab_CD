using Google.Protobuf.WellKnownTypes;
using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobShopAPI.Repository.Interfaces
{
    public class JobRepository : IJobRepository
    {
        //Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public JobRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CreateJob(Job simulation)
        {
            _db.Jobs.Add(simulation);
            return Save();
        }


        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool JobExists(string NameSimulation)
        {
            return _db.Jobs.Any(simu => simu.NameJob == NameSimulation);
        }

        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool JobExists(int IdSimulation)
        {
            return _db.Jobs.Any(simu => simu.IdJob == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public Task<ActionResult<ICollection<Job>>>  GetJobs()
        {
            var objDto = new List<JobDto>();
         

            var jobs = from opjob in _db.JobOperation
                       from job in _db.Jobs
                       from operation in _db.Operations
                       where job.IdJob == opjob.IdJob
                       select new
                       {
                          IdJob = job.IdJob,
                          NameJob = job.NameJob,
                          IdOperation = operation.IdOperation,
                          NameOperation = operation.OperationName
                       };

            var itens = jobs;
            return (Task<ActionResult<ICollection<Job>>>)itens;
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public Job GetJob(int IdSimulation)
        {
            return _db.Jobs.FirstOrDefault(simu => simu.IdJob == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateJob(Job simulation)
        {
            _db.Jobs.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteJob(Job simulation)
        {
            _db.Jobs.Remove(simulation);
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

///
using AutoMapper;
using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.Interfaces;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/// <summary>
/// Gestão de Simulações
/// </summary>
namespace JobShopAPI.Controllers
{
    [Route("api/Simulations")]
    [ApiController]
    [Authorize]
    public class SimulationsController : ControllerBase
    {

        private readonly ISimulationRepository _simuRepo;
        private readonly IMapper _mapper;
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operationRepo;
        private readonly ApplicationDbContext _db;

        public SimulationsController(ApplicationDbContext db, IMapper mapper, ISimulationRepository sim, IJobRepository job, IOperationRepository operationRepo)
        {
            _simuRepo = sim;
            _mapper = mapper;
            _job = job;
            _operationRepo = operationRepo;
            _db = db;

        }

        /// <summary>
        /// Buscar todos os trabalhos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult GetSimulations()
        {
         
            
               var simulations = from jobSimu in _db.JobSimulation
                           from job in _db.Jobs
                           from simulation in _db.Simulations
                      from user in _db.Users
                           where job.IdJob == jobSimu.IdJob && simulation.IdSimulation == jobSimu.IdSimulation && user.Id == simulation.Id
                           select new
                           {
                               IdUser = user.Id,
                               IdSimulation = simulation.IdSimulation,
                               NameSimulation = simulation.NameSimulation,
                               IdJob = job.IdJob,
                               NameJob = job.NameJob,
                           };
                
               
          
            var itens = simulations.OrderBy(u => u.IdUser).ToList();
            return Ok(itens);


        }


        /// <summary>
        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{simulationId:int}", Name = "GetSimulation")]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetSimulation(int idSimulation)
        {
  
            var simu = from jobSimu in _db.JobSimulation
                          join job in _db.Jobs.AsEnumerable() on jobSimu.IdJob equals job.IdJob
                          join simulation in _db.Simulations.AsEnumerable() on jobSimu.IdSimulation equals simulation.IdSimulation
                          join user in _db.Users.AsEnumerable() on simulation.Id equals user.Id
                          where  simulation.IdSimulation == idSimulation && user.Id == simulation.Id
                          select new
                          {
                              IdUser = user.Id,
                              IdSimulation = simulation.IdSimulation,
                              NameSimulation = simulation.NameSimulation,
                              IdJob = job.IdJob,
                              NameJob = job.NameJob,
                          };
            simu.FirstOrDefault(s => s.IdSimulation == idSimulation);
            var item = simu;
            return Ok(item);
        }

        /// <summary>
        /// Criar uma simulação
        /// </summary>
        /// <param name="simulationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<SimulationDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateSimulation([FromBody] CreateSimulationDto simulationDto)
        {
            //Dizer que a simulação pertence ao job
            if (simulationDto == null)
            {
                return BadRequest(ModelState); //Model State contém todos erros que são encontrados
            }
            if (_simuRepo.SimulationExists(simulationDto.NameSimulation)) // exists?
            {
                ModelState.AddModelError("", "Simulation Exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var simulationObj = _mapper.Map<Simulation>(simulationDto);

            if (!_simuRepo.CreateSimulation(simulationObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {simulationObj.IdSimulation}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSimulation", new { IdSimulation = simulationObj.IdSimulation }, simulationObj); //procura se foi criado e retorna 201 - OK
        }

        
        /// <summary>
        /// Atualizar uma simulação
        /// </summary>
        /// <param name="simulationId"></param>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPatch("{IdSimulation:int}", Name = "UpdateSimulation")]
        [ProducesResponseType(204, Type = typeof(List<SimulationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSimlation(int simulationId, [FromBody] SimulationDto nationalParkDto)
        {
            if (nationalParkDto == null || simulationId != nationalParkDto.IdSimulation)
            {
                return BadRequest(ModelState); //Model State contain all the errors if any encountered
            }
            var nationalParkObj = _mapper.Map<Simulation>(nationalParkDto);  //Map objectt

            if (!_simuRepo.UpdateSimulation(nationalParkObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {nationalParkObj.IdSimulation}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

       /// <summary>
       /// Apagar uma simulação
       /// </summary>
       /// <param name="simulationId"></param>
       /// <returns></returns>
        [HttpDelete("{IdSimulation:int}", Name = "DeleteSimulation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteSimulation(int simulationId)
        {
            if (!_simuRepo.SimulationExists(simulationId)) 
            {
                return NotFound();
            }
            else
            {
                var simulationObj = _simuRepo.GetSimulation(simulationId); 

                if (!_simuRepo.DeleteSimulation(simulationObj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {simulationObj.IdSimulation}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent();
        }
    }

}


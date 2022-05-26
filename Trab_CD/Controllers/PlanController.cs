using AutoMapper;
using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/Plans")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operation;
        private readonly IJobOperationRepository _jobOperation;
        private readonly IMachineOperationRepository _machineOperation;
        private readonly IPlanRepository _plan;
        private readonly IMachineRepository _machine;


        private readonly IMapper _mapper;

        public PlanController(ApplicationDbContext db, IPlanRepository plan, IMachineRepository machine, IMapper mapper, IJobRepository job, IOperationRepository operation, IJobOperationRepository jobOperation)
        {
            _db = db;
            _job = job;
           _mapper = mapper;
           _operation = operation;
           _jobOperation = jobOperation;
            _machine = machine;
            _plan = plan;
           

        }

       

        /// <summary>
        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{planId:int}", Name = "GetPlan")]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetPlan(int simulationId)
        {
                var job =  from plan in _db.Plan
                           
                           where plan.IdSimulation == simulationId
                           select new
                           {
                               IdJob = plan.IdJob,
                               IdOperation = plan.IdOperation,
                               IdMachine = plan.IdMachine,
                               IdSimulation = plan.IdSimulation,
                               IdPlan = plan.IdPlan,    
                            
                           };

                var item = job;
                return Ok(item);
            }


        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetPlan(int simulationId, int jobId, int operationId, int machineId)
        {
            var job = from plan in _db.Plan

                      where plan.IdSimulation == simulationId && plan.IdJob == jobId && plan.IdOperation == operationId && plan.IdMachine == machineId
                      select new
                      {
                          IdJob = plan.IdJob,
                          IdOperation = plan.IdOperation,
                          IdMachine = plan.IdMachine,
                          IdSimulation = plan.IdSimulation,
                          IdPlan = plan.IdPlan,

                      };

            var item = job;
            return Ok(item);
        }


        /// <summary>
        /// Criar trabalho
        /// </summary>
        /// <param name="planDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<JobDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreatePlan([FromBody] CreatePlanDto planDto)
        {
            if (planDto == null)
            {
                return BadRequest(ModelState); 
            }
            if (_plan.Exists(planDto.IdSimulation, planDto.IdJob, planDto.IdMachine, planDto.IdOperation)) // exists?
            {
                ModelState.AddModelError("", "Plan Exists");
                return StatusCode(404, ModelState);
            }
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var count = _plan.GetAll();
           
            foreach(var time in count)
            {
                if (time.IdMachine == planDto.IdMachine) {
                    int aux = planDto.initialTime;
                    int total = (planDto.finalTime - aux);
                    planDto.initialTime = time.finalTime;
                    planDto.finalTime = planDto.initialTime + total;
                }
            }
            var jobObj = _mapper.Map<Plan>(planDto);
            
            if (!_plan.Create(jobObj))
            {
               
                ModelState.AddModelError("", $"something went wrong when saving the record {jobObj.IdJob}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPlan", new { IdSimulation = jobObj.Simulation, IdJob = planDto.IdJob, IdOperation = planDto.IdOperation, IdMachine = planDto.IdMachine }, jobObj); 
        }


        /// <summary>
        /// Atualizar trabalho
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobDto"></param>
        /// <returns></returns>
        [HttpPatch("{planId:int}", Name = "UpdatePlan")]
        [ProducesResponseType(204, Type = typeof(List<JobDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdatePlan(int jobId, [FromBody] JobDto jobDto)
        {
            if (jobDto == null || jobId != jobDto.IdJob)
            {
                return BadRequest(ModelState); //Model State contain all the errors if any encountered
            }
            var jobObj = _mapper.Map<Job>(jobDto);  //Map objectt

            if (!_job.UpdateJob(jobObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {jobObj.IdJob}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Apagar trabalho
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        [HttpDelete("{planId:int}", Name = "DeletePlan")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeletePlan(int planId)
        {
            if (!_plan.Exists(planId)) //If not found
            {
                return NotFound();
            }
            else
            {
                var jobObj = _plan.Get(planId);  //Map objectt

                if (!_plan.Delete(jobObj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the plan");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }
}

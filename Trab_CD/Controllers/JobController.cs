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
    [Route("api/Jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operation;
        private readonly IJobOperationRepository _jobOperation;
        private readonly IMapper _mapper;

        public JobsController(ApplicationDbContext db,IMapper mapper, IJobRepository job, IOperationRepository operation, IJobOperationRepository jobOperation)
        {
           _job = job;
           _mapper = mapper;
            _operation = operation;
            _jobOperation = jobOperation;
            _db = db;

        }

        /// <summary>
        /// Buscar todos os trabalhos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult GetJobs()
        {
            var jobs = from opjob in _db.JobOperation
                       from job in _db.Jobs
                       from operation in _db.Operations
                       where job.IdJob == opjob.IdJob && operation.IdOperation == opjob.IdOperation
                       select new
                       {
                           IdJob = job.IdJob,
                           NameJob = job.NameJob,
                           IdOperation = operation.IdOperation,
                           NameOperation = operation.OperationName
                       };

            var itens = jobs.OrderBy(j => j.IdJob).ToList(); ;
            return Ok(itens);
        }

        /// <summary>
        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{jobId:int}", Name = "GetJob")]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetJob(int jobId)
        {
                var job =  from opjob in _db.JobOperation
                           join j in _db.Jobs .AsEnumerable() on opjob.IdJob equals j.IdJob
                           join p in _db.Operations.AsEnumerable() on opjob.IdOperation equals p.IdOperation
                           where j.IdJob == jobId
                          select new
                           {
                               IdJob = j.IdJob,
                               NameJob = j.NameJob,
                               IdOperation = p.IdOperation,
                               NameOperation = p.OperationName
                           };

                var item = job;
                return Ok(item);
            }


        

        /// <summary>
        /// Criar trabalho
        /// </summary>
        /// <param name="jobDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<JobDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateJob([FromBody] CreateJobDto jobDto)
        {
            if (jobDto == null)
            {
                return BadRequest(ModelState); //Model State contém todos erros que são encontrados
            }
            if (_job.JobExists(jobDto.NameJob)) // exists?
            {
                ModelState.AddModelError("", "Job Exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jobObj = _mapper.Map<Job>(jobDto);
            foreach (var operation in jobObj.JobOperation)
            {

                //if (jobObj.JobOperation != null) _operation.CreateOperation(operation);

            }
            if (!_job.CreateJob(jobObj))
            {
               
                ModelState.AddModelError("", $"something went wrong when saving the record {jobObj.IdJob}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetJob", new { IdJob= jobObj.IdJob }, jobObj); //procura se foi criado e retorna 201 - OK
        }


        /// <summary>
        /// Atualizar trabalho
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobDto"></param>
        /// <returns></returns>
        [HttpPatch("{jobId:int}", Name = "UpdateJob")]
        [ProducesResponseType(204, Type = typeof(List<JobDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateJob(int jobId, [FromBody] JobDto jobDto)
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
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpDelete("{jobId:int}", Name = "DeleteJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteJob(int jobId)
        {
            if (!_job.JobExists(jobId)) //If not found
            {
                return NotFound();
            }
            else
            {
                var jobObkj = _job.GetJob(jobId);  //Map objectt

                if (!_job.DeleteJob(jobObkj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {jobObkj.NameJob}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }
}

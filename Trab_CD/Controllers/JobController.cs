using AutoMapper;
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

        private readonly IJobRepository _job;
        private readonly IMapper _mapper;

        public JobsController(IMapper mapper, IJobRepository job)
        {
           _job = job;
           _mapper = mapper;

        }
        
        /// <summary>
        /// Buscar todos os trabalhos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetJobs()
        {
            var objList = _job.GetJobs();
            var objDto = new List<JobDto>();

            foreach (var item in objList)
            {
                objDto.Add(_mapper.Map<JobDto>(item));
            }
            return Ok(objList);
        }

        /// <summary>
        /// Buscar trabalho pelo Id
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
            var obj = _job.GetJob(jobId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<OperationDto>(obj);
            return Ok(objDto);
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
            if (jobDto == null || jobId != jobDto.IdOperation)
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
                    ModelState.AddModelError("", $"something went wrong when deleting the record {jobObkj.IdOperation}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }
}

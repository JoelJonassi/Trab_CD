///
using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/// <summary>
/// Gestão de Simulações
/// </summary>
namespace JobShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationsController : ControllerBase
    {

        private readonly ISimulationRepository _simuRepo;
        private readonly IMapper _mapper;

        public SimulationsController(IMapper mapper, ISimulationRepository sim)
        {
            _simuRepo = sim;
            _mapper = mapper;

        }
       
        /// <summary>
        /// Get List of Simulations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SimulationDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetSimulations()
        {
            var objList = _simuRepo.GetSimulations();
            var objDto = new List<SimulationDto>();

            foreach (var item in objList)
            {
                objDto.Add(_mapper.Map<SimulationDto>(item));
            }
            return Ok(objList);
        }

        
        [HttpGet("{simulationId:int}", Name = "GetSimulation")]
        [ProducesResponseType(200, Type = typeof(List<SimulationDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        //[Authorize]
        public IActionResult GetSimulation(int simulationId)
        {
            var obj = _simuRepo.GetSimulation(simulationId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<SimulationDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// Cria simulação
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
            if (simulationDto == null)
            {
                return BadRequest(ModelState); //Model State contém todos erros que são encontrados
            }
            if (_simuRepo.SimulationExists(simulationDto.IdSimulation)) // exists?
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

        

        [HttpPatch("{idSimulation:int}", Name = "UpdateSimulation")]
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
        /// delete
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <returns></returns>
        [HttpDelete("{idSimulation:int}", Name = "DeleteSimulation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteSimulation(int simulationId)
        {
            if (!_simuRepo.SimulationExists(simulationId)) //If not found
            {
                return NotFound();
            }
            else
            {
                var simulationObj = _simuRepo.GetSimulation(simulationId);  //Map objectt

                if (!_simuRepo.DeleteSimulation(simulationObj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {simulationObj.IdSimulation}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }

}


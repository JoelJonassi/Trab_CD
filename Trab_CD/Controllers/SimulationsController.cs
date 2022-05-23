///
using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
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
        [AllowAnonymous]
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

        /// <summary>
        /// Busca de simulação pelo Id
        /// </summary>
        /// <param name="simulationId"></param>
        /// <returns></returns>
        [HttpGet("{IdSimulation:int}", Name = "GetSimulation")]
        [ProducesResponseType(200, Type = typeof(List<SimulationDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
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


using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.Interfaces;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/Operations")]
    [ApiController]
    public class OperationsController : ControllerBase
    {

        private readonly IOperationRepository _operation;
        private readonly IMapper _mapper;

        public OperationsController(IMapper mapper, IOperationRepository operation)
        {
            _operation = operation;
            _mapper = mapper;

        }

       /// <summary>
       /// Buscar todas as operações existentes
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOperations()
        {
            var objList = _operation.GetOperations();
            var objDto = new List<OperationDto>();

            foreach (var item in objList)
            {
                objDto.Add(_mapper.Map<OperationDto>(item));
            }
            return Ok(objList);
        }



        /// <summary>
        /// Buscar operação pelo Id
        /// </summary>
        /// <param name="IdOperation"></param>
        /// <returns></returns>
        [HttpGet("{IdOperation:int}", Name = "GetOperation")]
        [ProducesResponseType(200, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetOperation(int IdOperation)
        {
            var obj = _operation.GetOperation(IdOperation);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<OperationDto>(obj);
            return Ok(objDto);
        }


        /// <summary>
        /// Criar Simulação
        /// </summary>
        /// <param name="simulationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateSimulation([FromBody] CreateOperationDto simulationDto)
        {
            if (simulationDto == null)
            {
                return BadRequest(ModelState); //Model State contém todos erros que são encontrados
            }
            if (_operation.OperationExists(simulationDto.OperationName)) // exists?
            {
                ModelState.AddModelError("", "Simulation Exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var simulationObj = _mapper.Map<Operation>(simulationDto);

            if (!_operation.CreateOperation(simulationObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {simulationObj.IdOperation}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetOperation", new { IdOperation = simulationObj.IdOperation }, simulationObj); //procura se foi criado e retorna 201 - OK
        }

       
        /// <summary>
        /// Atualizar uma Simulação
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationDto"></param>
        /// <returns></returns>
        [HttpPatch("{IdOperation:int}", Name = "UpdateOperation")]
        [ProducesResponseType(204, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateOperation(int operationId, [FromBody] OperationDto operationDto)
        {
            if (operationDto == null || operationId != operationDto.IdOperation)
            {
                return BadRequest(ModelState); //Model State contain all the errors if any encountered
            }
            var operationobj = _mapper.Map<Operation>(operationDto);  //Map objectt

            if (!_operation.UpdateOperation(operationobj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {operationobj.IdOperation}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

       /// <summary>
       /// Apagar uma Simulação
       /// </summary>
       /// <param name="operationId"></param>
       /// <returns></returns>
        [HttpDelete("{IdOperation:int}", Name = "DeleteOperation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteOperation(int operationId)
        {
            if (!_operation.OperationExists(operationId)) //If not found
            {
                return NotFound();
            }
            else
            {
                var simulationObj = _operation.GetOperation(operationId);  //Map objectt

                if (!_operation.DeleteOperation(simulationObj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {simulationObj.IdOperation}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }
}

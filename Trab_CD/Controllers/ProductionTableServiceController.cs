using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/PTService")]
    [ApiController]
    public class ProductionTableServiceController : ControllerBase
    {
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operation;
        private readonly IMachineRepository _machine;
        private readonly IMapper _mapper;


        public ProductionTableServiceController(IMapper mapper, IJobRepository job, IOperationRepository operation, IMachineRepository machine)
        {
            _job = job;
            _mapper = mapper;
            _machine = machine;
            _operation = operation;

        }

        /// <summary>
        /// Get List of Simulations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetTable()
        {
            var objList = _job.GetJobs();
            var objDto = new List<JobDto>();

            foreach (var item in objList)
            {
                objDto.Add(_mapper.Map<JobDto>(item));
            }
            return Ok(objList);
        }


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


        /*[HttpGet]
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
        }*/

        /*  /// <summary>
          /// Insere uma uma máquina e tempo para uma operação especifica de um trabalho
          /// </summary>
          /// <param name="operationDto"></param>
          /// <returns></returns>
          [HttpPut]
          [ProducesResponseType(StatusCodes.Status201Created)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]

          public IActionResult CreateOperation([FromBody] CreateOperationDto operationDto)
          {

                  if (_machine.MachineExists(operationDto.IdMachine))
                  {
                      var operationObj = _mapper.Map<Operation>(operationDto);
                      _operation.CreateOperation(operationObj);
                      return Ok(operationDto);

                  }          
                  return StatusCode(404, ModelState);
          }
          */

        /// <summary>
        /// Cria máquina para determinada operação e insere o tempo na operação
        /// </summary>
        /// <param name="machineDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult InsertMachineInOperation(int IdOperation, [FromBody] CreateMachineDto machineDto)
        {
            var obj = _operation.GetOperation(IdOperation);

            if (_machine.MachineExists(machineDto.MachineName))
            {
                if (_operation.OperationExists(machineDto.IdOperation))
                {
                    var machineObj = _mapper.Map<Machine>(machineDto);
                    obj.time = machineDto.Time;
                    _operation.UpdateOperation(obj);
                    _machine.CreateMachine(machineObj);
                    return Ok(machineDto);
                }

            }
            return StatusCode(404, ModelState);
        }

         
         
         [HttpPatch("{IdOperation:int}", Name = "UpdateOperation")]
         [ProducesResponseType(StatusCodes.Status201Created)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult UpdateOperation(int idOperation, [FromBody] UpdateOperationDto operationDto)
         {

             if (_machine.MachineExists(operationDto.IdMachine))
             {
                 var operationObj = _mapper.Map<Operation>(operationDto);
                 _operation.UpdateOperation(operationObj);
                 return Ok(operationDto);
             }
             return StatusCode(404, ModelState);
         }


    
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="IdMachine"></param>
        /// <param name="machineDto">Copy of nationalParkId</param>
        /// <returns></returns>
        [HttpPatch("{machineId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204, Type = typeof(List<UpdateMachineDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult UpdateMachine(int IdMachine, [FromBody] UpdateMachineDto machineDto)
        {
            if (machineDto == null || IdMachine != machineDto.IdMachine)
            {
                return BadRequest(ModelState); //Model State contain all the errors if any encountered
            }
            var machineObj = _mapper.Map<Machine>(machineDto);  //Map objectt

            if (!_machine.UpdateMachine(machineObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {machineObj.MachineName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



        /// <summary>
        /// Elimanar máquina da lista
        /// </summary>
        /// <param name="IdMachine"></param>
        /// <returns></returns>
        [HttpDelete("{idMachine:int}", Name = "DeleteMachine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMachine(int IdMachine)
        {
            if (!_machine.MachineExists(IdMachine)) //If not found
            {
                return NotFound();
            }
            else
            {
                var machineObj = _machine.GetMachine(IdMachine);  //Map objectt
                if (!_machine.DeleteMachine(machineObj))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {machineObj.IdMachine}");
                    return StatusCode(500, ModelState);
                }
                //depois de apagar a máquina tem de ir as operações e retirar a máquina apagada nas operações
                foreach (var item in _operation.GetOperations())
                {
                    //item.IdMachine = NULL;
                }
            }
            return NoContent(); // if not receive any content the park was deleted.
        }
    }
}

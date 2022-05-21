/**
 * Nome: Joel Jonassi
 * Nome : Aurelien
 * 
 * 
 */
using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JobShopAPI.Controllers
{
    [Route("api/Machines")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IOperationRepository _operation;
        private readonly IMachineRepository _machine;
        private readonly IMapper _mapper;


        public MachineController(IMapper mapper,IOperationRepository operation, IMachineRepository machine)
        {
            _mapper = mapper;
            _machine = machine;
            _operation = operation;

        }
        
        /*
        /// <summary>
        ///Atualiza os dados da operação na máquina
        /// </summary>
        /// <param name="idOperation"></param>
        /// <param name="operationDto"></param>
        /// <returns></returns>
         [HttpPatch("{IdMachine:int}", Name = "UpdateMachine")]
         [ProducesResponseType(StatusCodes.Status201Created)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult UpdateOperationInMachine(int idOperation, [FromBody] UpdateOperationDto operationDto)
         {

             if (_machine.MachineExists(operationDto.IdMachine))
             {
                 var operationObj = _mapper.Map<Operation>(operationDto);
                 _operation.UpdateOperation(operationObj);
                 return Ok(operationDto);
             }
             return StatusCode(404, ModelState);
         }*/


    
      /// <summary>
      /// Atualiza máquina
      /// </summary>
      /// <param name="IdMachine"></param>
      /// <param name="machineDto"></param>
      /// <returns></returns>
        [HttpPatch("{IdMachine:int}", Name = "UpdateMachine")]
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
        /// Insere operação na máquina
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





        /// <summary>
        /// Elimina uma máquina
        /// </summary>
        /// <param name="IdMachine"></param>
        /// <returns></returns>
        [HttpDelete("{IdMachine:int}", Name = "DeleteMachine")]
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

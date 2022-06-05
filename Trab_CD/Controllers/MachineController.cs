/**
 * Nome: Joel Jonassi
 * Nome : Aurelien
 * 
 * 
 */
using AutoMapper;
using JobShopAPI.Data;
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
        private readonly ApplicationDbContext _db;


        public MachineController(ApplicationDbContext db, IMapper mapper,IOperationRepository operation, IMachineRepository machine)
        {
            _mapper = mapper;
            _machine = machine;
            _operation = operation;
            _db = db;

        }

        /// <summary>
        /// Buscar todos os trabalhos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult GetMachines()
        {
            var machines = from maOp in _db.MachineOperation
                       from machine in _db.Machines
                       from operation in _db.Operations
                       where maOp.IdMachine == machine.IdMachine && maOp.IdOperation == operation.IdOperation
                       select new
                       {
                           IdOperation = operation.IdOperation,
                           NameOperation = operation.OperationName,
                           IdMachine = machine.IdMachine,
                           NameMachine = machine.MachineName,
                           
                       };
            
        
        
            var itens = machines.OrderBy(m => m.IdOperation).ToList(); ;
            return Ok(itens);
        }

        /// <summary>
        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{machineId:int}", Name = "GetMachine")]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetMachine(int machineId)
        {
            var machine = from maOp in _db.MachineOperation
                          join op in _db.Operations.AsEnumerable() on maOp.IdOperation equals op.IdOperation
                          join m in _db.Machines.AsEnumerable() on maOp.IdMachine equals m.IdMachine
                          where m.IdMachine == machineId
                          select new
                          {
                              IdMachine = m.IdMachine,
                              NameMachine = m.MachineName,
                              IdOperation = op.IdOperation,
                              NameOperation = op.OperationName
                          };
            machine.FirstOrDefault(j => j.IdMachine == machineId);
            var item = machine;
            return Ok(item);
        }



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

using AutoMapper;
using JobShopAPI.Data;
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
    [Authorize] //Só pode utilizar este controlador o utilizador que estiver autenticado
    public class OperationsController : ControllerBase
    {

        private readonly IOperationRepository _operation;
        private readonly IMachineRepository _machine;
        private readonly IMachineOperationRepository _machineOperation;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;


        public OperationsController(ApplicationDbContext db, IMapper mapper, IMachineRepository machine, IOperationRepository operation, IMachineOperationRepository machineOperation)
        {
            _operation = operation;
            _mapper = mapper;
            _machine = machine;
            _machineOperation = machineOperation;
            _db = db;

        }

        /// <summary>
        /// Buscar todos os trabalhos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult GetOperations()
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
                               Time = machine.time,
                           };



            var itens = machines.OrderBy(m => m.IdOperation).ToList(); ;
            return Ok(itens);
        }

        /// <summary>
        /// Buscar trabalho pelo Id, Mostra o tempo, a máquina e a operação assoaciada ao job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{operationId:int}", Name = "GetOperation")]
        [ProducesResponseType(200, Type = typeof(List<JobDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetOperation(int idOperation)
        {
            var machine = from maOp in _db.MachineOperation
                          join op in _db.Operations.AsEnumerable() on maOp.IdOperation equals op.IdOperation
                          join m in _db.Machines.AsEnumerable() on maOp.IdMachine equals m.IdMachine
                          where op.IdOperation == idOperation
                          select new
                          {
                              IdOperation = op.IdOperation,
                              NameOperation = op.OperationName,
                              IdMachine = m.IdMachine,
                              NameMachine = m.MachineName,
                              Time = m.time,
                          };
            machine.FirstOrDefault(j => j.IdMachine == idOperation);
            var item = machine;
            return Ok(item);
        }



        /// <summary>
        /// Criar Operação
        /// </summary>
        /// <param name="operationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateOperation([FromBody] CreateOperationDto operationDto)
        {
            if (operationDto == null)
            {
                return BadRequest(ModelState); //Model State contém todos erros que são encontrados
            }
            if (_operation.OperationExists(operationDto.OperationName)) 
            {
                ModelState.AddModelError("", "Operation Exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var operationObj = _mapper.Map<Operation>(operationDto);
            if (!_operation.CreateOperation(operationObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {operationObj.IdOperation}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetOperation", new { IdOperation = operationObj.IdOperation }, operationObj); //procura se foi criado e retorna 201 - OK
        }


        /// <summary>
        ///  Inserir máquinas na operação
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
            if (!_operation.OperationExists(operationDto.IdOperation)) return BadRequest("Operação inserida não existe");
            if (!_machine.MachineExists(operationDto.IdMachine)) return BadRequest("Máquina não existe");

            if (!_operation.UpdateOperation(operationobj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {operationobj.IdOperation}");
                return StatusCode(500, ModelState);
            }
            var operationMachineObj = new MachineOperation()
            {
                IdMachine = operationDto.IdMachine,
                IdOperation = operationDto.IdOperation,
            };
            if (!_machineOperation.Create(operationMachineObj))
            {
                ModelState.AddModelError("", $"something went wrong when create machine for operation");
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

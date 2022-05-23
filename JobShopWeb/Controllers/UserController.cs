using JobShopAPI.Repository.Interfaces;
using JobShopWeb.Models;
using JobShopWeb.Models.ViewModel;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
     
        private readonly IAccountRepository _user;

        public UserController(IAccountRepository user)
        {
        _user = user;
;
        }

        /// <summary>
        /// View para listar utilizadores
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            string AccountPath = "https://localhost:7032/api/Users";
            UserVM listofUsers = new UserVM()
            {
             
                UserList = await _user.GetAllAsync(AccountPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listofUsers);
        }

       public async Task <IActionResult> GetAllUsers()
        {
            string AccountPath = "https://localhost:7032/api/Users";
            return Json(new { data = await _user.GetAllAsync(AccountPath, HttpContext.Session.GetString("JWToken")) });
        }

       
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            User obj = new User();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update

            obj = await _user.GetAsync(UriAPI.AccountPath + "idUser?idUser=", id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// Função assincrona que recebe por parametro um objeto do tipo user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(User obj, UpdateUserDto userUp)
        {
            if (obj.Role == null) obj.Role = "common";
            
            CreateUserDto userDto = new CreateUserDto()
            {
                Username = obj.Username,
                Password = obj.Password,     
                Role = obj.Role,
            };
            UpdateUserDto userUpdateDto = new UpdateUserDto()
            {
                Username = userUp.Username,
                NewPassword = userUp.NewPassword,
                OldPassword = userUp.OldPassword,
            };
            if (ModelState.IsValid)
            {
                
               
                if (obj.Id == 0)
                {
                    await _user.RegisterAsync(UriAPI.AccountPath + "register", userDto, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _user.UpdateAsync(UriAPI.AccountPath + "update", userUpdateDto, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _user.DeleteAsync(UriAPI.AccountPath + "Delete" + "?userId=", id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Apagado com Sucesso" });
            }
            return Json(new { success = false, message = "Falha na remoção" });
        }
    }

   

}


/*

//jQuery.ajax

[Authorize(Roles = "Admin")]
public async Task<IActionResult> Upsert(int? id)
{
    IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));
    IEnumerable<Operation> operationList = await _operation.GetAllAsync(UriAPI.OperationsApiPath, HttpContext.Session.GetString("JWToken"));
    IEnumerable<Machine> machineList = await _machine.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken"));

    SimulationVM objVM = new SimulationVM()
    {
        JobList = jobList.Select(i => new SelectListItem
        {
            Text = i.NameJob,
            Value = i.IdJob.ToString()
        }),
        Simulation = new Simulation(),

    };

           /* SimulationVM objVM1 = new SimulationVM()
            {
                
                OperationList = operationList.Select(i => new SelectListItem
                {
                    Text = i.OperationName,
                    Value = i.IdOperation.ToString()
                }),
                Simulation = new Simulation(),

            };*/
/*SimulationVM objVM2 = new SimulationVM()
{

    MachineList = machineList.Select(i => new SelectListItem
    {
        Text = i.MachineName,
        Value = i.IdMachine.ToString()
    }),
    Simulation = new Simulation(),

};*/
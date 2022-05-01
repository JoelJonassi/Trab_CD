///
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
        // GET: api/<GSimulationsController>
        [HttpGet]
        public IEnumerable<string> GetSimulation()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<GSimulationsController>/5
        [HttpGet("{id}")]
        public string GetSimulations(int id)
        {
            return "value";
        }

        // POST api/<GSimulationsController>
        [HttpPost]
        public void PostSimulation([FromBody] string value)
        {
        }

        // PUT api/<GSimulationsController>/5
        [HttpPut("{id}")]
        public void PutSimulation(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GSimulationsController>/5
        [HttpDelete("{id}")]
        public void DeleteSimulation(int id)
        {
        }
    }
}

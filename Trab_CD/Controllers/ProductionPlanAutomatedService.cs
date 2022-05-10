using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanAutomatedService : ControllerBase
    {
        // GET: api/<ProductionPlanAutomatedService>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductionPlanAutomatedService>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductionPlanAutomatedService>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductionPlanAutomatedService>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductionPlanAutomatedService>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanService : ControllerBase
    {
        // GET: api/<ProductionPlanService>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductionPlanService>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductionPlanService>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductionPlanService>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductionPlanService>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

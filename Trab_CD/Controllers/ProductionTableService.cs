using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionTableService : ControllerBase
    {
        // GET: api/<TableProductionService>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TableProductionService>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TableProductionService>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TableProductionService>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TableProductionService>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

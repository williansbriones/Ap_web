using api_proyecto_web.Modelos;
using api_proyecto_web.Servicios;
using api_proyecto_web.Servicios.Implementacion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_proyecto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_compras : ControllerBase
    {
        IcrudCompras<compras> compras = new ComprasServicios();


        // GET: api/<Controller_compras>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<Controller_compras>/5
        [HttpGet("{id}",Name = "GetIndividual")]
        public compras GetIndividual(int id)
        {
            return compras.individual(id);
        }

        // POST api/<Controller_compras>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Controller_compras>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Controller_compras>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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
        IcrudCompras<compras> servicio_compras = new ComprasServicios();

        

        // GET api/<Controller_compras>/5
        [HttpGet("Ordenes_de_compra/{id_compra}")]
        public compras GetIndividual(int id_compra)
        {
            return servicio_compras.individual(id_compra);
        }

        [HttpGet("Ordenes_de_clientes/{id_cliente}",Name ="GetCompras")]
        public IList<compras> GetCompras(int id_cliente)
        {
            return servicio_compras.compras_cliente(id_cliente);
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

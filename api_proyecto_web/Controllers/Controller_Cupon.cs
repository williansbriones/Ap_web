using api_proyecto_web.Servicios;
using api_proyecto_web.Servicios.Implementacion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_proyecto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Cupon : ControllerBase

    {
        IcrudCupon CuponServicios = new CuponServicios();



        // GET: api/<Controller_Cupon>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Controller_Cupon>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Controller_Cupon>
        [HttpPost]
        public void Post(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            CuponServicios.Crearcupon(Nombre, CantidadDesuento, Codigo, Cantidad_limite, FechaInicio, FechaTermino);
        }

        // PUT api/<Controller_Cupon>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Controller_Cupon>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

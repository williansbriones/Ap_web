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


        //devuelve informacion 
        // GET: api/<Controller_Cupon>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //devuelve informacion 
        // GET api/<Controller_Cupon>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        //crear recursos
        // POST api/<Controller_Cupon>
        [HttpPost("crear_cupon ")]
        public void Post(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            CuponServicios.Crearcupon(Nombre, CantidadDesuento, Codigo, Cantidad_limite, FechaInicio, FechaTermino);
        }
        //editar resursos
        // PUT api/<Controller_Cupon>/5
        [HttpPut("Modificarcupon")]
        public void Modificarcupon(int id_cupon, string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite)
        {
            CuponServicios.Modificarcupon(id_cupon, Nombre, CantidadDesuento, Codigo, Cantidad_limite);
        }
        //eliminar recursos
        // DELETE api/<Controller_Cupon>/5
        [HttpDelete("eliminar_cupon ")]
        public void Delete(int id)
        {
            CuponServicios.Eliminar_cupon(id);
        }
        [HttpPut("desabilitarcupon")]
        public void Des_Habilitar (int id_cupon)
        {
            CuponServicios.Des_Habilitar(id_cupon);
        }

        [HttpPut("habilitarcupon")]
        public void Habilitar (int id_cupon)
        {
            CuponServicios.Habilitar_cupon(id_cupon);
        }
    }
}

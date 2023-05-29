using api_proyecto_web.Modelos;
using api_proyecto_web.Servicios;
using api_proyecto_web.Servicios.Implementacion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_proyecto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_usuario : ControllerBase
    {
        IcrudUsuario usuario = new UsuarioServicio();


        // GET: api/<Controller_usuario>
        [HttpGet("InformacionUsuario")] //controller lo unico que realiza en la obtencion de datos
        public Usuario Get()
        {
            return usuario.informacionUsuario();
        }

        // GET api/<Controller_usuario>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Controller_usuario>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Controller_usuario>/5
        [HttpPut("CambioContraseña")]
        public void Put([FromBody] string contraseña)
        {
            usuario.CambiarContraseña(contraseña);
        }

        // DELETE api/<Controller_usuario>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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
        Servicios.IcrudUsuario usuario = new Servicios.Implementacion.UsuarioServicio();


        // GET: api/Controller_usuario/InformacionUsuario
        [HttpGet("InformacionUsuario")] //controller lo unico que realiza en la obtencion de datos
        public Usuario Get()
        {
            return usuario.informacionUsuario();
        }

        // POST api/Controller_usuario/Inicio_Sesion
        [HttpPost("Inicio_Sesion")]
        public void Post( string email, string contraseña)
        {
            usuario.InicioSesion(email,contraseña);
        }

        // POST api/Controller_usuario/CrearUsuario
        [HttpPost("CrearUsuario")]
        public void CrearUsuario(string nombnre, string apellidos, string telefono, string email, string direccion, string comuna, string contraseña)
        {
            usuario.CrearUsuario(nombnre, apellidos, telefono, email, direccion, comuna, contraseña);
        }
        //POST api/Controller_usuario/Cerrar_sesion
        [HttpPost("Cerrar_sesion")]
        public void CerrarSesion()
        {
            usuario.cerrarSesion();
        }
        // PUT api/Controller_usuario/CambioContraseña/{contraseña_antigua}/{contraseña}
        [HttpPut("CambioContraseña")]
        public void Put(string contraseña_antigua, string contraseña)
        {
            usuario.CambiarContraseña(contraseña_antigua, contraseña);
        }
        //PUT api/Controller_usuario/EditarInformacion/{nombre}/{apellido}/{telefono}/{email}/{direccion}/{comuna}
        [HttpPut("EditarInformacion")]
        public void EditarUsuario(string nombre, string apellido, string telefono, string email, string direccion, string comuna)
        {
            usuario.EditarUsuario(nombre, apellido, telefono, email, direccion, comuna);
        }

        
    }
}

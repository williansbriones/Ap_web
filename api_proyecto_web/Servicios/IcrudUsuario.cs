using api_proyecto_web.Modelos;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;

namespace api_proyecto_web.Servicios
{
    public interface IcrudUsuario
    {
        public void CambiarContraseña(string constraseña_antigua, string Contraseña);
        public Usuario informacionUsuario();
        public void InicioSesion(string correo, string contraseña);
        public void CrearUsuario(string nombnre, string apellido, string telefono, string email, string direccion, string comuna, string contraseña);
        public void EditarUsuario(string nombnre, string apellido, string telefono, string email, string direccion, string comuna);
        public void cerrarSesion(); 
    }
}

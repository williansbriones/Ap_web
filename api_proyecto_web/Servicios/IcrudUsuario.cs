using api_proyecto_web.Modelos;

namespace api_proyecto_web.Servicios
{
    public interface IcrudUsuario
    {
        public void CambiarContraseña(string constraseña_antigua, string Contraseña);
        public Usuario informacionUsuario();
        public void InicioSesion(string correo, string contraseña);
    }
}

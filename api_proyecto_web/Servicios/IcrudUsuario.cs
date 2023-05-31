using api_proyecto_web.Modelos;

namespace api_proyecto_web.Servicios
{
    public interface UsuarioServicio
    {
        public void CambiarContraseña(String Contraseña);

        public Usuario informacionUsuario();


    }
}

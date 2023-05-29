using api_proyecto_web.Modelos;

namespace api_proyecto_web.Servicios
{
    public interface IcrudUsuario
    {
        public void CambiarContraseña(String Contraseña);

        public Usuario informacionUsuario();


    }
}

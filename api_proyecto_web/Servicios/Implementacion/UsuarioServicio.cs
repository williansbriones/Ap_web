using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class UsuarioServicio : Servicios.UsuarioServicio
    {

        public static  Usuario UsuarioIniciado = new Usuario(1,new imagen(),Tipo_usuario.Usuario_cliente,"Willians","briones","95094532","wi.briones@duocuc.cl","vilone 2157","pepe123ndd");
        static IList<Usuario> ListaUsuarios = new List<Usuario>();




        public void CambiarContraseña(string Contraseña)
        {
            UsuarioIniciado.Contraseña = Contraseña;
        }

        public Usuario informacionUsuario()
        {
            return UsuarioIniciado;
        }
    }
}

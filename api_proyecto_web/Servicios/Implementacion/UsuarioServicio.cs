using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class UsuarioServicio : IcrudUsuario
    {
        static Usuario Usuario1 = new Usuario();

        public UsuarioServicio() 
        {
            if (Usuario1.Nombre == string.Empty) {
                Usuario1.Foto_perfil = new imagen();
                Usuario1.tipo_Usuario = Tipo_usuario.Usuario_cliente;
                Usuario1.Nombre = "Willians";
                Usuario1.Apellido = "Briones";
                Usuario1.telefono = "950945356";
                Usuario1.Email = "wi.briones@duocuc.cl";
                Usuario1.Direccion = "vivolen 2757";
                Usuario1.Lista_compras = new List<compras>();
                Usuario1.Contraseña = "Web_206323";
            }
        }


        public void CambiarContraseña(string Contraseña)
        {
            Usuario1.Contraseña = Contraseña;
        }

        public Usuario informacionUsuario()
        {
            return Usuario1;
        }
    }
}

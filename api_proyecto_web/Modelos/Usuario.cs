using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Modelos
{
    public class Usuario
    {
        public int Id { get; set; } 
        public imagen Foto_perfil { get; set; }
        public Tipo_usuario tipo_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Contraseña { get; set; }
        public Usuario()
        {
            this.Id = new int();
            this.Foto_perfil = new imagen();
            this.tipo_Usuario = Tipo_usuario.Invitado;
            this.Nombre = string.Empty;
            this.Apellido = string.Empty;
            this.telefono = string.Empty;
            this.Email = string.Empty;
            this.Direccion = string.Empty;
            this.Contraseña = string.Empty;
        }
        public Usuario(int id,imagen foto, Tipo_usuario tipo_Usuario, string nombre, string apellido, string telefono, string email, string direccion, string contraseña)
        {
            this.Id = id;
            this.Foto_perfil = foto;
            this.tipo_Usuario = tipo_Usuario;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.telefono = telefono;
            this.Email = email;
            this.Direccion = direccion;
            this.Contraseña = contraseña;
        }


    }
}

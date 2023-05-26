using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Modelos
{
    public class Usuario
    {
        public imagen Foto_perfil { get; set; }
        public Tipo_usuario tipo_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public IList<compras> Lista_compras { get; set; }

        public Usuario()
        {
            this.Foto_perfil = new imagen();
            this.tipo_Usuario = Tipo_usuario.Invitado;
            this.Nombre = string.Empty;
            this.Apellido = string.Empty;
            this.telefono = string.Empty;
            this.Email = string.Empty;
            this.Direccion = string.Empty;
            this.Lista_compras = new List<compras>();
        }



    }
}

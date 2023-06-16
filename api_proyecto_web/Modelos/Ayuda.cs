namespace api_proyecto_web.Modelos
{
    public class Ayuda
    {
        public string email { get; set; }
        public string descripcion { get; set; }
        public string fecha_ingreso { get; set; } 

        public Ayuda()
        {
            email = string.Empty;
            descripcion = string.Empty;
            fecha_ingreso = string.Empty;
        }

    }
}

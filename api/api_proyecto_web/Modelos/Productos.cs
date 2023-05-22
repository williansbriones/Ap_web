namespace api_proyecto_web.Modelos
{
    public class Productos
    {

        public int Id { get; set; }
        public string nombre { get; set; }
        public string caracteristicas { get; set; }
        public int precio { get; set; }
        public imagen imagen1 { get; set; }
        public imagen imagen2 { get; set; }
        public imagen imagen3 { get; set; }
        public imagen imagen4 { get; set; }
        public imagen imagen5 { get; set; }
        public int oferta { get; set; }
        public Productos()
        {
            Id = new int();
            nombre = string.Empty;
            caracteristicas = string.Empty;
            precio = new int();
            imagen1 = new imagen();
            imagen2 = new imagen();
            imagen3 = new imagen();
            imagen4 = new imagen();
            imagen5 = new imagen();
            oferta = new int();
        }

    }
}

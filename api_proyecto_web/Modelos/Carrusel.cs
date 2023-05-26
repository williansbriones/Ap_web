namespace api_proyecto_web.Modelos
{
    public class Carrusel
    {
        public int Id { get; set; }
        public imagen Imagen1 { get; set; }
        public imagen Imagen2 { get; set; }
        public imagen Imagen3 { get; set; }
        public string Descripcion1 { get; set;}
        public string Descripcion2 { get; set;}
        public string Descripcion3 { get;set;}
        public IList<int> TipoProducto { get; set; }

        public Carrusel()
        {
            this.Id = new int();
            this.Imagen1 = new imagen();
            this.Imagen2 = new imagen();
            this.Imagen3 = new imagen();
            this.Descripcion1 = string.Empty;
            this.Descripcion2 = string.Empty;
            this.Descripcion3 = string.Empty;
            this.TipoProducto = new List<int>();
        }

    }
}

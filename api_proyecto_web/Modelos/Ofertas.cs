namespace api_proyecto_web.Modelos
{
    public class Ofertas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int CantidadDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }


        public Ofertas()
        {
            this.Id = new int();
            this.Nombre = string.Empty;
            this.Estado = false;
            this.CantidadDescuento = new int();
            this.FechaInicio = new DateTime();
            this.FechaTermino = new DateTime();
            
        }
    }
}

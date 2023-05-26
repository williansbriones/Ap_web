namespace api_proyecto_web.Modelos
{
    public class Cupon
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public int CantidadDesuento { get; set; }
        public string Codigo { get; set; }
        public int Cantidad_limite { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }


        public Cupon()
        {
            this.Id = new int();
            this.Nombre = string.Empty;
            this.CantidadDesuento = new int();
            this.Codigo = string.Empty;
            this.FechaInicio = new DateTime();
            this.FechaTermino = new DateTime();
        }


    }
}

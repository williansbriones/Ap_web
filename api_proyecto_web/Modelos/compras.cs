namespace api_proyecto_web.Modelos
{
    public class compras
    {


        public int id_compra { get; set; }
        public int id_usuario { get; set; }
        public IList<Productos> lista_productos { get; set; }
        public DateTime Fecha_compra { get; set; }
        public DateTime Fecha_entrega  { get; set; }
        public EstadoCompra estado_compra { get; set;}

        public compras()
        {
            this.id_compra = new int();
            this.id_usuario = new int();
            this.lista_productos = new List<Productos>();
            this.Fecha_compra = DateTime.Now;
            this.Fecha_entrega = DateTime.Now.AddDays(3);
            this.estado_compra = EstadoCompra.cancelado;
        }

    }
}

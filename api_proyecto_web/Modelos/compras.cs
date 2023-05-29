using api_proyecto_web.Modelos.@enum;
using System.Xml.Schema;

namespace api_proyecto_web.Modelos
{
    public class compras
    {


        public int id_compra { get; set; }
        public int id_usuario { get; set; }
        public IList<Productos> lista_productos { get; set; }
        public DateTime Fecha_compra { get; set; }
        public DateTime Fecha_entrega { get; set; }
        public EstadoCompra Estado_compra { get; set; }
        public int Total => lista_productos.Sum(x => x.precio);
        public int Descuento => this.Total*(lista_productos.Sum(x => x.Ofertas.CantidadDescuento)/100);
        public int SubTotal => this.Total-this.Descuento;
        public int CantidadProductos => this.lista_productos.Count;
        
        
        public compras()
        {
            this.id_compra = new int();
            this.id_usuario = new int();
            this.lista_productos = new List<Productos>();
            this.Fecha_compra = DateTime.Now;
            this.Fecha_entrega = DateTime.Now.AddDays(3);
            this.Estado_compra = EstadoCompra.cancelado;
        }

    }
}

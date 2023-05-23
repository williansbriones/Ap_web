using api_proyecto_web.Modelos;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class ComprasServicios : IcrudCompras<compras>
    {
        static IList<compras> ListaCompras = new List<compras>();//creacion de una lista de compras
        static IList<Productos> ListaProductos = new List<Productos>();//creacion de una lista de productos

        public ComprasServicios() //Poblacion de la lista de compras "ListaCompras"
        {
            int contador = 1;
         if (contador == 1) { 
                ListaCompras.Add(new compras
                {
                    id_compra = 1,
                    id_usuario = 1,
                    lista_productos = ListaProductos,
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    estado_compra = EstadoCompra.aceptado,

                });
                ListaCompras.Add(new compras
                {
                    id_compra = 2,
                    id_usuario = 1,
                    lista_productos = ListaProductos,
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    estado_compra = EstadoCompra.cancelado,

                });
                ListaCompras.Add(new compras
                {
                    id_compra = 3,
                    id_usuario = 2,
                    lista_productos = ListaProductos,
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    estado_compra = EstadoCompra.empacado,

                });
                contador = 2;
            }
        }
        IList<compras> IcrudCompras<compras>.compras_cliente(int id_cliente)//metodo para obtener las compras de un cliente en especifico
        {
            IList<compras> ComprasCliente = new List<compras>();
            foreach (compras com in ListaCompras)
            {
                if (com.id_usuario == id_cliente)
                {
                    ComprasCliente.Add(com);
                }
            }

            return ComprasCliente;
        }

        compras IcrudCompras<compras>.individual(int id_compra)//metodo para obtener una compra en especifico
        {
            compras Compra_individual = new compras();
            foreach (compras compra in ListaCompras)
            {
                if (compra.id_compra == id_compra)
                {
                    Compra_individual = compra;
                }
                    
            }

            return Compra_individual;
        }
    }
}

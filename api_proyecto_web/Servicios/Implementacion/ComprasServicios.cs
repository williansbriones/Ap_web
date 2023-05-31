using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class ComprasServicios : IcrudCompras<compras>
    {
        static IList<compras> ListaCompras = new List<compras>();//creacion de una lista de compras
        static Ofertas Ofertas = new Ofertas();//Oferta
        
        public ComprasServicios() //Poblacion de la lista de compras "ListaCompras"
        {
            
            if (ListaCompras.Count == 0) {
                Ofertas.Nombre = "procesadores descuento";
                Ofertas.Id = 1;
                Ofertas.CantidadDescuento = 10;
                Ofertas.Estado = true;
                Ofertas.FechaInicio = DateTime.Now;
                Ofertas.FechaTermino = DateTime.Now;

                ListaCompras.Add(new compras
                {
                    id_compra = 1,
                    id_usuario = 1,
                    lista_productos = new List<Productos>(),
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    Estado_compra = EstadoCompra.aceptado,

                });//primera compra de la lista compra 
                ListaCompras[0].lista_productos.Add(new Productos
                {
                    Id = 1,
                    tipo_producto = 1,
                    nombre = "core I5",
                    caracteristicas = "procesador",
                    precio = 500000,
                    imagen1 = new imagen(),
                    imagen2 = new imagen(),
                    imagen3 = new imagen(),
                    imagen4 = new imagen(),
                    imagen5 = new imagen(),
                    Cupon = new Cupon(),
                    Ofertas = Ofertas,
                }); ;//primer producto para la primera compra (Core I5)
                ListaCompras.Add(new compras
                {
                    id_compra = 2,
                    id_usuario = 1,
                    lista_productos = new List<Productos>(),
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    Estado_compra = EstadoCompra.cancelado,

                });//segunda compra de la "lista compra"
                ListaCompras[1].lista_productos.Add(new Productos 
                {
                    Id = 2,
                    nombre = "core I7",
                    caracteristicas = "procesador",
                    precio = 700000,
                    imagen1 = new imagen(),
                    imagen2 = new imagen(),
                    imagen3 = new imagen(),
                    imagen4 = new imagen(),
                    imagen5 = new imagen(),
                    Cupon = new Cupon(),
                    Ofertas = Ofertas,
                });//primer producto de la segunda compra lista compra (Core I7)
                ListaCompras[1].lista_productos.Add(new Productos
                {
                    Id = 1,
                    tipo_producto = 1,
                    nombre = "core I5",
                    caracteristicas = "procesador",
                    precio = 500000,
                    imagen1 = new imagen(),
                    imagen2 = new imagen(),
                    imagen3 = new imagen(),
                    imagen4 = new imagen(),
                    imagen5 = new imagen(),
                    Cupon = new Cupon(),
                    Ofertas = Ofertas,
                });//segundo producto de la segunda lista compra compra (Core I5)
                ListaCompras.Add(new compras
                {
                    id_compra = 3,
                    id_usuario = 2,
                    lista_productos = new List<Productos>(),
                    Fecha_compra = DateTime.Now,
                    Fecha_entrega = DateTime.Now.AddDays(3),
                    Estado_compra = EstadoCompra.empacado,

                });//tercera compra de la "lista compra"
                ListaCompras[2].lista_productos.Add(new Productos
                {
                    Id = 3,
                    nombre = "GTX 1250 IT",
                    caracteristicas = "Tarjetas Graficas",
                    precio = 1200000,
                    imagen1 = new imagen(),
                    imagen2 = new imagen(),
                    imagen3 = new imagen(),
                    imagen4 = new imagen(),
                    imagen5 = new imagen(),
                    Cupon = new Cupon(),
                    Ofertas = new Ofertas(),
                });//primer producto de la tercera compra lista compra (GTX 1250 IT)

            }
        }
        IList<compras> IcrudCompras<compras>.BusquedaComprasCliente(int id_cliente)//metodo para obtener las compras de un cliente en especifico
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

        compras IcrudCompras<compras>.BusquedaCompraIndividual(int id_compra)//metodo para obtener una compra en especifico
        {
            compras Compra_individual = new compras();
            foreach (compras compra in ListaCompras)
            {
                if (compra.id_compra == id_compra)
                {
                    Compra_individual = compra;
                    break;
                }
                    
            }

            return Compra_individual;
        }

        public IList<compras> BusquedaComprasClienteIniciado()
        {
            IList<compras> ComprasCliente = new List<compras>();



            foreach (compras com in ListaCompras)
            {
                if (com.id_usuario == UsuarioServicio.UsuarioIniciado.Id)
                {
                    ComprasCliente.Add(com);
                }
            }

            return ComprasCliente;
        }
    }
}

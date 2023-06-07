using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System.Data;
using System.Drawing;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class ComprasServicios : IcrudCompras<compras>
    {
        
        static DBConText.Connection db = new DBConText.Connection();

        string con = "User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;";
        public ComprasServicios(string con)
        {
            db = new DBConText.Connection(con);
        }

        public ComprasServicios()
        {
        }

        IList<compras> IcrudCompras<compras>.BusquedaComprasCliente(int id_cliente)//metodo para obtener las compras de un cliente en especifico
        {
            IList<compras> ComprasCliente = new List<compras>();


            return ComprasCliente;
        }

        compras IcrudCompras<compras>.BusquedaCompraIndividual(int id_compra)//metodo para obtener una compra en especifico
        {
            IList<Productos> ListaProductos = new List<Productos>();
            compras CompraIndividual = new compras();

            String Query = String.Format("select c.id_compra as id_compra, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) where  c.id_compra = " + id_compra);

            DataTable dt = db.Execute(Query);
            
            CompraIndividual.id_compra = Convert.ToInt32(dt.Rows[0]["id_compra"]);
            CompraIndividual.id_usuario = Convert.ToInt32(dt.Rows[0]["id_usuario"]);
            foreach (DataRow dr in dt.Rows) 
            { 
                ListaProductos.Add(new Productos { 
                    Id = Convert.ToInt32(dr["id_tipo_producto"]),
                    tipo_producto = Tipo_Producto.Teclados,
                    nombre = "a",
                    caracteristicas = "as",
                    precio = 1,
                    imagen1 = new imagen(),
                    imagen2 = new imagen(),
                    imagen3 = new imagen(),
                    imagen4 = new imagen(),
                    imagen5 = new imagen()
                });
            }
            CompraIndividual.Descuento = 1;
            CompraIndividual.SubTotal = 1;
            Cupon cupon = new Cupon();


            return CompraIndividual;
        }

        public IList<compras> BusquedaComprasClienteIniciado()
        {
            IList<compras> ComprasCliente = new List<compras>();

            return ComprasCliente;
        }
    }
}

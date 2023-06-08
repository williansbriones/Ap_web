using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System.Data;
using System.Collections.Generic;
using System.Drawing;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class ComprasServicios : IcrudCompras<compras>
    {
        static DBConText.Connection db = new DBConText.Connection();
        public ComprasServicios()
        {
            db = new DBConText.Connection("User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;");
        }
        IList<compras> IcrudCompras<compras>.BusquedaComprasCliente(int id_cliente)//metodo para obtener las compras de un cliente en especifico
        {
            IList<compras> listaCompras = new List<compras>();
            int index_compra = 0;
            int index_pedido = -1;
            string Query = String.Format("select dp.cantidad as cantidad_producto, nvl(to_char(cu.fecha_expira),to_char(sysdate)) as fecha_termino, nvl(to_char(cu.fecha_inicio),to_char(sysdate)) as fecha_inicio, nvl(cu.cant_uso, 0) as cantidad_uso, nvl(cu.codigo, 'Sin codigo') as condigo_desc, nvl(cu.cant_descuento,0) as descuento_cupon, nvl(cu.nombre,'Sin cupon') as nombre_cupon, nvl(cu.id_cupon,0) as id_cupon, c.id_compra as id_compra, c.id_estado_compra as id_estado_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_usuario = " + id_cliente + " order by c.id_compra");
            string Query2 = string.Format("select id_compra as id_compra, id_usuario as id_usuario from compra c where id_usuario = " + id_cliente + " order by c.id_compra");
            DataTable dt1 = db.Execute(Query);
            DataTable dt2 = db.Execute(Query2);

            foreach (DataRow dr1 in dt2.Rows) 
            {
                listaCompras.Add(new compras
                {
                    id_compra = Convert.ToInt32(dr1["id_compra"]),
                    id_usuario = Convert.ToInt32(dr1["id_usuario"]),
                });

                foreach (DataRow dr2 in dt1.Select(string.Format("id_compra = " + dr1["id_compra"])))
                {
                    listaCompras[index_compra].lista_productos.Add(new Productos
                    {
                        Id = Convert.ToInt32(dr2["id_producto"]),
                        tipo_producto = (api_proyecto_web.Modelos.@enum.Tipo_Producto)Convert.ToInt32(dr2["id_tipo_producto"]),
                        nombre = (string)dr2["nombre_producto"],
                        caracteristicas = (string)dr2["caracteristicas"],
                        precio = Convert.ToInt32(dr2["precio"]),
                        imagen1 = new imagen(),
                        imagen2 = new imagen(),
                        imagen3 = new imagen(),
                        imagen4 = new imagen(),
                        imagen5 = new imagen(),
                        cantidad = Convert.ToInt32(dr2["cantidad_producto"])

                    });
                    index_pedido++;
                }
                
                Console.WriteLine("cupon: "+dt1.Rows[index_pedido]["id_cupon"]);
                Console.WriteLine("-----------------------------");
                listaCompras[index_compra].cupon.Id = Convert.ToInt32(dt1.Rows[index_pedido]["id_cupon"]);
                listaCompras[index_compra].cupon.Nombre = dt1.Rows[index_pedido]["nombre_cupon"].ToString();
                listaCompras[index_compra].cupon.CantidadDesuento = Convert.ToInt32(dt1.Rows[index_pedido]["descuento_cupon"]);
                listaCompras[index_compra].cupon.Codigo = dt1.Rows[index_pedido]["condigo_desc"].ToString();
                listaCompras[index_compra].cupon.Cantidad_limite = Convert.ToInt32(dt1.Rows[index_pedido]["cantidad_uso"]);
                listaCompras[index_compra].cupon.FechaInicio = DateTime.Parse(dt1.Rows[index_pedido]["fecha_inicio"].ToString());
                listaCompras[index_compra].cupon.FechaTermino = DateTime.Parse(dt1.Rows[index_pedido]["fecha_termino"].ToString());

                listaCompras[index_compra].Estado_compra = (EstadoCompra)Convert.ToInt32(dt1.Rows[index_pedido]["id_estado_compra"]);
                listaCompras[index_compra].Descuento = ((int)(listaCompras[index_compra].Total * (listaCompras[index_compra].cupon.CantidadDesuento / 100.000)));
                listaCompras[index_compra].SubTotal = listaCompras[index_compra].Total - listaCompras[index_compra].Descuento;

                index_compra++;

            }


           

            return listaCompras;
        }

        compras IcrudCompras<compras>.BusquedaCompraIndividual(int id_compra)//metodo para obtener una compra en especifico
        {
            IList<Productos> ListaProductos = new List<Productos>();
            compras CompraIndividual = new compras();
            //////////// Implementacion de las instancias en instancias//////////////
            CompraIndividual.lista_productos = ListaProductos;

            String Query = String.Format("select dp.cantidad as cantidad_producto, nvl(to_char(cu.fecha_expira),to_char(sysdate)) as fecha_termino, nvl(to_char(cu.fecha_inicio),to_char(sysdate)) as fecha_inicio, nvl(cu.cant_uso, 0) as cantidad_uso, nvl(cu.codigo, 'Sin codigo') as condigo_desc, nvl(cu.cant_descuento,0) as descuento_cupon, nvl(cu.nombre,'Sin cupon') as nombre_cupon, nvl(cu.id_cupon,0) as id_cupon, c.id_compra as id_compra, c.id_estado_compra as id_estado_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_compra = " + id_compra);

            DataTable dt = db.Execute(Query);
            if (dt.Rows.Count > 0 )
            { 
                CompraIndividual.id_compra = Convert.ToInt32(dt.Rows[0]["id_compra"]);
                CompraIndividual.id_usuario = Convert.ToInt32(dt.Rows[0]["id_usuario"]);
                foreach (DataRow dr in dt.Rows)
                { 
                    ListaProductos.Add(new Productos { 
                        Id = Convert.ToInt32(dr["id_producto"]),
                        tipo_producto = (api_proyecto_web.Modelos.@enum.Tipo_Producto)Convert.ToInt32(dr["id_tipo_producto"]),
                        nombre = (string)dr["nombre_producto"],
                        caracteristicas = (string)dr["caracteristicas"],
                        precio = Convert.ToInt32(dr["precio"]),
                        imagen1 = new imagen(),
                        imagen2 = new imagen(),
                        imagen3 = new imagen(),
                        imagen4 = new imagen(),
                        imagen5 = new imagen(),
                        cantidad = Convert.ToInt32(dr["cantidad_producto"])
                    });
                }
                CompraIndividual.cupon.Id = Convert.ToInt32(dt.Rows[0]["id_cupon"]);
                CompraIndividual.cupon.Nombre = dt.Rows[0]["nombre_cupon"].ToString();
                CompraIndividual.cupon.CantidadDesuento = Convert.ToInt32(dt.Rows[0]["descuento_cupon"]);
                CompraIndividual.cupon.Codigo = dt.Rows[0]["condigo_desc"].ToString();
                CompraIndividual.cupon.Cantidad_limite = Convert.ToInt32(dt.Rows[0]["cantidad_uso"]);
                CompraIndividual.cupon.FechaInicio = DateTime.Parse(dt.Rows[0]["fecha_inicio"].ToString());
                CompraIndividual.cupon.FechaTermino = DateTime.Parse(dt.Rows[0]["fecha_termino"].ToString());

                CompraIndividual.Estado_compra = (EstadoCompra)Convert.ToInt32(dt.Rows[0]["descuento_cupon"]);
                CompraIndividual.Descuento = ((int)(CompraIndividual.Total *(CompraIndividual.cupon.CantidadDesuento/100.000)));
                CompraIndividual.SubTotal = CompraIndividual.Total - CompraIndividual.Descuento; 
            }
            return CompraIndividual;
        }

        public IList<compras> BusquedaComprasClienteIniciado()
        {
            IList<compras> ComprasCliente = new List<compras>();

            return ComprasCliente;
        }
    }
}

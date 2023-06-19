using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System.Data;
using System.Globalization;


namespace api_proyecto_web.Servicios.Implementacion
{
    public class ComprasServicios : IcrudCompras<compras>
    {

        public static compras CarroDeCompra = CarroDeCompra !=null ? CarroDeCompra : new compras(); //carro de compra que contendra los productos que se quieran comprar

        static DBConText.Connection db = new DBConText.Connection();
        IList<compras> IcrudCompras<compras>.BusquedaComprasCliente(int id_cliente)//metodo para obtener las compras de un cliente en especifico
        {
            //obtencion de informacion para rellenar  con las lista y las compras para entregar la informacion de las compras
            IList<compras> listaCompras = new List<compras>();
            string Query = String.Format("select dp.cantidad as cantidad_producto, c.id_compra as id_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_usuario = " + id_cliente + " order by c.id_compra");
            string Query2 = string.Format("select c.id_estado_compra as id_estado_compra, c.id_compra as id_compra, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon from compra c LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where c.id_usuario = "+id_cliente);
            DataTable dt1 = db.Execute(Query);//ejecucion de la consulta
            DataTable dt2 = db.Execute(Query2);//ejecucion de la consulta

            if(dt2.Rows.Count > 0) //evalua si las consultas tienen iformacion que entregar
            { 
                foreach (DataRow dr in dt2.Rows) //for para iterar las compras existentes
                {
                    listaCompras.Add(new compras //ingreso de todas las compras asociadas a un cliente a la lista
                    {
                        id_usuario = id_cliente,
                        Estado_compra = (EstadoCompra)Convert.ToInt32(dr["id_estado_compra"]),
                        id_compra = Convert.ToInt32(dr["id_compra"]),
                        Fecha_entrega = DateTime.Parse(dr["fecha_termino"].ToString()),
                        Fecha_compra = DateTime.Parse(dr["fecha_inicio"].ToString()),
                        cupon = new Cupon{
                            Id = Convert.ToInt32(dr["id_cupon"]),
                            Cantidad_limite = Convert.ToInt32(dr["cantidad_uso"]),
                            Codigo = dr["condigo_desc"].ToString(),
                            CantidadDesuento = Convert.ToInt32(dr["descuento_cupon"]),
                            Nombre = dr["nombre_cupon"].ToString()
                        }

                    });
                }
                foreach (compras com in listaCompras) //ingreso de productos a las compras individuales 
                {
                    com.lista_productos = (from DataRow dr in dt1.Rows
                                           where Convert.ToInt32(dr["id_compra"]) == com.id_compra
                                           select new Productos()
                                           {
                                               Id = Convert.ToInt32(dr["id_producto"]),
                                               cantidad = Convert.ToInt32(dr["cantidad_producto"]),
                                               tipo_producto = (Tipo_Producto)Convert.ToInt32(dr["id_tipo_producto"]),
                                               nombre = dr["nombre_producto"].ToString(),
                                               caracteristicas = dr["caracteristicas"].ToString(),
                                               precio = Convert.ToInt32(dr["precio"])
                                           }
                                           ).ToList();
                }
            }
            //resumen de los loops y como estas rellenaban las listas //resumen listaCompras(tiene muchas) ----> compra(tiene) -----> listaproductos(tiene muchos productos) --->productos//
            return listaCompras;
        }
        compras IcrudCompras<compras>.BusquedaCompraIndividual(int id_compra)//metodo para obtener una compra en especifico
        {
            IList<Productos> ListaProductos = new List<Productos>(); //lista de productos que se ingresara en la compra
            compras CompraIndividual = new compras();//compra que se rellenara con la informacion de la Query
            CompraIndividual.lista_productos = ListaProductos; //ingreso de la listaProductos en la compra
            //consulta de la compra en especifico con todos los productos relacionados
            String Query = String.Format("select dp.cantidad as cantidad_producto, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon, c.id_compra as id_compra, c.id_estado_compra as id_estado_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_compra = " + id_compra);
            DataTable dt = db.Execute(Query); //ejecucion de la consulta

            if (dt.Rows.Count > 0 )//validador de que la consulta entregue datos
            { 
                //ingreso de la informacion dentro del objeto compra
                CompraIndividual.id_compra = Convert.ToInt32(dt.Rows[0]["id_compra"]); 
                CompraIndividual.id_usuario = Convert.ToInt32(dt.Rows[0]["id_usuario"]); 
                foreach (DataRow dr in dt.Rows) //ingreso de informacion dentro de la listaProductos agragando a su vez "Productos"
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
                //mas ingformacion de la compra
                CompraIndividual.cupon.Id = Convert.ToInt32(dt.Rows[0]["id_cupon"]);
                CompraIndividual.cupon.Nombre = dt.Rows[0]["nombre_cupon"].ToString();
                CompraIndividual.cupon.CantidadDesuento = Convert.ToInt32(dt.Rows[0]["descuento_cupon"]);
                CompraIndividual.cupon.Codigo = dt.Rows[0]["condigo_desc"].ToString();
                CompraIndividual.cupon.Cantidad_limite = Convert.ToInt32(dt.Rows[0]["cantidad_uso"]);
                CompraIndividual.cupon.FechaInicio = DateTime.Parse(dt.Rows[0]["fecha_inicio"].ToString());
                CompraIndividual.cupon.FechaTermino = DateTime.Parse(dt.Rows[0]["fecha_termino"].ToString());

                CompraIndividual.Estado_compra = (EstadoCompra)Convert.ToInt32(dt.Rows[0]["id_estado_compra"]);
            }
            //en resumen compra(tiene) ----> listaProductos(tiene muchos) ----> productos
            return CompraIndividual;
        }
        public IList<compras> BusquedaComprasClienteIniciado() //metodo para obtener las compras de los clientes que se encuentren con la secion iniciada
        {
            int id_cliente = UsuarioServicio.UsuarioIniciado.Id; //busqueda del id en la clase usuarioServicio
            IList<compras> listaCompras = new List<compras>();   //lista que entregara la informacion de las compras del usurio que tenga la sesion activa 
            //Querys para obtener la informacion de las compras del usuario
            string Query = String.Format("select dp.cantidad as cantidad_producto, c.id_compra as id_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_usuario = " + id_cliente + " order by c.id_compra");
            string Query2 = string.Format("select c.id_estado_compra as id_estado_compra, c.id_compra as id_compra, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon from compra c LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where c.id_usuario = " + id_cliente);
            DataTable dt1 = db.Execute(Query);
            DataTable dt2 = db.Execute(Query2);

            if (dt2.Rows.Count > 0)//validador de que exista compras
            {
                foreach (DataRow dr in dt2.Rows)//ingreso de compras a la lista
                {
                    listaCompras.Add(new compras
                    {
                        id_usuario = id_cliente,
                        Estado_compra = (EstadoCompra)Convert.ToInt32(dr["id_estado_compra"]),
                        id_compra = Convert.ToInt32(dr["id_compra"]),
                        Fecha_entrega = DateTime.Parse(dr["fecha_termino"].ToString()),
                        Fecha_compra = DateTime.Parse(dr["fecha_inicio"].ToString()),
                        cupon = new Cupon
                        {
                            Id = Convert.ToInt32(dr["id_cupon"]),
                            Cantidad_limite = Convert.ToInt32(dr["cantidad_uso"]),
                            Codigo = dr["condigo_desc"].ToString(),
                            CantidadDesuento = Convert.ToInt32(dr["descuento_cupon"]),
                            Nombre = dr["nombre_cupon"].ToString()
                        }

                    });
                }
                foreach (compras com in listaCompras)//ingreso de productos a la lista productos
                {
                    com.lista_productos = (from DataRow dr in dt1.Rows 
                                           where Convert.ToInt32(dr["id_compra"]) == com.id_compra
                                           select new Productos()
                                           {
                                               Id = Convert.ToInt32(dr["id_producto"]),
                                               cantidad = Convert.ToInt32(dr["cantidad_producto"]),
                                               tipo_producto = (Tipo_Producto)Convert.ToInt32(dr["id_tipo_producto"]),
                                               nombre = dr["nombre_producto"].ToString(),
                                               caracteristicas = dr["caracteristicas"].ToString(),
                                               precio = Convert.ToInt32(dr["precio"])
                                           }
                                           ).ToList();
                }
            }
            return listaCompras;
        }
        public void Agregarproducto(int id_producto, int cantidad) // metodo que agregar productos al carro de compra 
        {

            if (UsuarioServicio.UsuarioIniciado.Id == 0) //codigo que genera un usuario el cual se incia en caso de no tener un usuario iniciado
            {
                string query = "SELECT NEXT VALUE FOR SQ_id_usuario as numero";
                DataTable dt_id_nuevo_usuario = new DataTable();
                dt_id_nuevo_usuario = db.Execute(query);
                string query_ingreso_usuario = "begin tran INSERT INTO usuario VALUES(" + dt_id_nuevo_usuario.Rows[0]["numero"] + ", '', '', '', 0, '" + dt_id_nuevo_usuario.Rows[0]["numero"] + "', '" + dt_id_nuevo_usuario.Rows[0]["numero"] + "', '', '', '') COMMIT TRAN";
                db.Execute(query_ingreso_usuario);
                UsuarioServicio.UsuarioIniciado.Id = Convert.ToInt32(dt_id_nuevo_usuario.Rows[0]["numero"]);
            }
            //Querys que buscan el producto indicado 
            string Query_Ingresar_producto = "select id_producto as id_producto, id_tipo_producto as id_tipo_producto, nombre as nombre, caracteristicas as caracteristicas, precio as precio from Producto where id_producto = "+ id_producto;
            DataTable dt_Producto_ingreso = db.Execute(Query_Ingresar_producto);
            int indice; //variable para realizar busquedas en las listas 
            //Query que busca informacion si el usuario ya tiene un carro de compra en la base de datos activo actualmente
            String Query_Obtener_compra = String.Format("select c.id_estado_compra as id_estado_compra, c.id_compra as id_compra, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon from compra c LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where c.id_usuario = " + UsuarioServicio.UsuarioIniciado.Id + " and c.ID_ESTADO_COMPRA = 1 ");
            DataTable dt_obtener_compra = db.Execute(Query_Obtener_compra);
            if (dt_obtener_compra.Rows.Count > 0)//valida si el usuario tiene carro de compras
            {
                //relleno de informacion la compra del usuario
                CarroDeCompra.id_compra = Convert.ToInt32(dt_obtener_compra.Rows[0]["id_compra"]);
                CarroDeCompra.id_usuario = UsuarioServicio.UsuarioIniciado.Id;
                CarroDeCompra.cupon.Id = Convert.ToInt32(dt_obtener_compra.Rows[0]["id_cupon"]);
                CarroDeCompra.cupon.Nombre = dt_obtener_compra.Rows[0]["nombre_cupon"].ToString();
                CarroDeCompra.cupon.CantidadDesuento = Convert.ToInt32(dt_obtener_compra.Rows[0]["descuento_cupon"]);
                CarroDeCompra.cupon.Codigo = dt_obtener_compra.Rows[0]["condigo_desc"].ToString();
                CarroDeCompra.cupon.Cantidad_limite = Convert.ToInt32(dt_obtener_compra.Rows[0]["cantidad_uso"]);
                CarroDeCompra.Estado_compra = (EstadoCompra)Convert.ToInt32(dt_obtener_compra.Rows[0]["id_estado_compra"]);
            }
            else // genera carro de compras en caso que el usuario no tenga
            {
                string query_Create_compra = "begin tran INSERT INTO compra VALUES (NEXT VALUE FOR SQ_id_compra," + UsuarioServicio.UsuarioIniciado.Id + ",0,0,1,NULL) commit tran";
                DataTable dt_creacion_compra = db.Execute(query_Create_compra);
                dt_creacion_compra = db.Execute(Query_Obtener_compra);

                CarroDeCompra.id_compra = Convert.ToInt32(dt_creacion_compra.Rows[0]["id_compra"]);
                CarroDeCompra.id_usuario = UsuarioServicio.UsuarioIniciado.Id;
                CarroDeCompra.cupon.Id = Convert.ToInt32(dt_creacion_compra.Rows[0]["id_cupon"]);
                CarroDeCompra.cupon.Nombre = dt_creacion_compra.Rows[0]["nombre_cupon"].ToString();
                CarroDeCompra.cupon.CantidadDesuento = Convert.ToInt32(dt_creacion_compra.Rows[0]["descuento_cupon"]);
                CarroDeCompra.cupon.Codigo = dt_creacion_compra.Rows[0]["condigo_desc"].ToString();
                CarroDeCompra.cupon.Cantidad_limite = Convert.ToInt32(dt_creacion_compra.Rows[0]["cantidad_uso"]);
                CarroDeCompra.Estado_compra = (EstadoCompra)Convert.ToInt32(dt_creacion_compra.Rows[0]["id_estado_compra"]);
            }
            //obtiene los productos que contenga las compras
            string Query_obtencion_productos = String.Format("select dp.cantidad as cantidad_producto, c.id_compra as id_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_usuario = " + UsuarioServicio.UsuarioIniciado.Id + " order by c.id_compra");
            DataTable dt_obtencion_producto = db.Execute(Query_obtencion_productos);
            if (dt_obtencion_producto.Rows.Count > 0) // valida si tiene produtos asociados a las compras del usuario 
            {
                //ingreso y filtro de las compra del cliente la cual se encuentren dentro del carro de compras
                CarroDeCompra.lista_productos = (from DataRow dr in dt_obtencion_producto.Rows
                                                 where Convert.ToInt32(dr["id_compra"]) == CarroDeCompra.id_compra
                                                 select new Productos()
                                                {
                                                    Id = Convert.ToInt32(dr["id_producto"]),
                                                    cantidad = Convert.ToInt32(dr["cantidad_producto"]),
                                                    tipo_producto = (Tipo_Producto)Convert.ToInt32(dr["id_tipo_producto"]),
                                                    nombre = dr["nombre_producto"].ToString(),
                                                    caracteristicas = dr["caracteristicas"].ToString(),
                                                    precio = Convert.ToInt32(dr["precio"])
                                                }
                                                ).ToList();
            }

            try
            {
                if (dt_Producto_ingreso.Rows.Count > 0) //verifica que exista informacion en la consulta 
                {
                    //busqueda del indica para aumentar la cantidad del producto por si existe dentro del carro de compra
                    indice = CarroDeCompra.lista_productos.Select((item, index) => new
                    {
                        itemname = item,
                        indexx = index,
                    }).Where(x => x.itemname.Id == id_producto)
                    .First()
                    .indexx;
                    int precio;
                    precio = CarroDeCompra.lista_productos[indice].precio; //obtencion de precio para ajustar el valor segun la cantidad en la base de datos
                    Console.WriteLine("indice encontrado" + indice);
                    CarroDeCompra.lista_productos[indice].cantidad = CarroDeCompra.lista_productos[indice].cantidad + cantidad;
                    Console.WriteLine(CarroDeCompra.lista_productos[indice].cantidad);
                    //acutualizacion de la cantidad aumentada y de los valores
                    string QueryActualizacionPorductos = "begin tran UPDATE detalle_compra set sub_total = " + precio * CarroDeCompra.lista_productos[indice].cantidad + ", cantidad = " + CarroDeCompra.lista_productos[indice].cantidad + " where id_compra = "+ CarroDeCompra.id_compra + " and id_producto = "+ id_producto + "commit tran";
                    DataTable dt = db.Execute(QueryActualizacionPorductos);
                    string QueryActualizacionCompra = "begin tran UPDATE COMPRA set descuento = "+CarroDeCompra.Descuento +" , total = "+CarroDeCompra.Total+" where id_compra = "+CarroDeCompra.id_compra+" commit tran";
                    db.Execute(QueryActualizacionCompra);
                }
            }
            catch // en caso de que no exista producto asociado a la compra 
            {
                CarroDeCompra.lista_productos.Add (new Productos { 
                    Id = Convert.ToInt32(dt_Producto_ingreso.Rows[0]["id_producto"]),
                    tipo_producto = (Tipo_Producto)Convert.ToInt32(dt_Producto_ingreso.Rows[0]["id_tipo_producto"]),
                    nombre = dt_Producto_ingreso.Rows[0]["nombre"].ToString(),
                    caracteristicas = dt_Producto_ingreso.Rows[0]["caracteristicas"].ToString(),
                    precio = Convert.ToInt32(dt_Producto_ingreso.Rows[0]["precio"]),
                    cantidad = cantidad
                });
                Console.WriteLine("producto agregado: "+dt_Producto_ingreso.Rows[0]["nombre"].ToString());
                //aumenta la cantidad de producto y genera un fila en la base de datos
                string QueryIngresoDetalle = "begin tran INSERT INTO detalle_compra VALUES ( " + CarroDeCompra.id_compra + ", "+ Convert.ToInt32(dt_Producto_ingreso.Rows[0]["id_producto"]) + " , "+ cantidad+ " , "+(cantidad * Convert.ToInt32(dt_Producto_ingreso.Rows[0]["precio"])) + " ) commit tran";
                DataTable dt = db.Execute(QueryIngresoDetalle);
                string QueryActualizacionCompra = "begin tran UPDATE COMPRA set descuento = " + CarroDeCompra.Descuento + " , total = " + CarroDeCompra.Total + " where id_compra = " + CarroDeCompra.id_compra + " commit tran";
                db.Execute(QueryActualizacionCompra);
            }    
        }
        public void EliminarProducto(int id_producto, int cantidad) //metodo que elimina cantidad de productos que se encuentren en el carro de compra 
        {
            //querys que obtiene informacion del producto
            string Query_Ingresar_producto = "select id_producto as id_producto, id_tipo_producto as id_tipo_producto, nombre as nombre, caracteristicas as caracteristicas, precio as precio from Producto where id_producto = " + id_producto;
            DataTable dt_Producto_ingreso = db.Execute(Query_Ingresar_producto);
            int indice; //indice para buscar productos
            //query que busque si el usuario tiene o no una compra en el carro de compras
            String Query_Obtener_compra = String.Format("select c.id_estado_compra as id_estado_compra, c.id_compra as id_compra, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon from compra c LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where c.id_usuario = " + UsuarioServicio.UsuarioIniciado.Id + " and c.ID_ESTADO_COMPRA = 1 ");
            DataTable dt_obtener_compra = db.Execute(Query_Obtener_compra);
            if (dt_obtener_compra.Rows.Count > 0)// valida si existe carro de compras
            {
                //ingreso de informacion al carro de compras 
                CarroDeCompra.id_compra = Convert.ToInt32(dt_obtener_compra.Rows[0]["id_compra"]);
                CarroDeCompra.id_usuario = UsuarioServicio.UsuarioIniciado.Id;
                CarroDeCompra.cupon.Id = Convert.ToInt32(dt_obtener_compra.Rows[0]["id_cupon"]);
                CarroDeCompra.cupon.Nombre = dt_obtener_compra.Rows[0]["nombre_cupon"].ToString();
                CarroDeCompra.cupon.CantidadDesuento = Convert.ToInt32(dt_obtener_compra.Rows[0]["descuento_cupon"]);
                CarroDeCompra.cupon.Codigo = dt_obtener_compra.Rows[0]["condigo_desc"].ToString();
                CarroDeCompra.cupon.Cantidad_limite = Convert.ToInt32(dt_obtener_compra.Rows[0]["cantidad_uso"]);
                CarroDeCompra.Estado_compra = (EstadoCompra)Convert.ToInt32(dt_obtener_compra.Rows[0]["id_estado_compra"]);
            }
            //obtiene informacion de los productos por de las compras asociadas al usuario iniciado
            string Query_obtencion_productos = String.Format("select dp.cantidad as cantidad_producto, c.id_compra as id_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_usuario = " + UsuarioServicio.UsuarioIniciado.Id + " order by c.id_compra");
            DataTable dt_obtencion_producto = db.Execute(Query_obtencion_productos);
            if (dt_obtencion_producto.Rows.Count > 0) //valida si hay informacion de la query 
            {
                //ingreso de productos al carro de comrpas
                CarroDeCompra.lista_productos = (from DataRow dr in dt_obtencion_producto.Rows
                                                 where Convert.ToInt32(dr["id_compra"]) == CarroDeCompra.id_compra
                                                 select new Productos()
                                                 {
                                                     Id = Convert.ToInt32(dr["id_producto"]),
                                                     cantidad = Convert.ToInt32(dr["cantidad_producto"]),
                                                     tipo_producto = (Tipo_Producto)Convert.ToInt32(dr["id_tipo_producto"]),
                                                     nombre = dr["nombre_producto"].ToString(),
                                                     caracteristicas = dr["caracteristicas"].ToString(),
                                                     precio = Convert.ToInt32(dr["precio"])
                                                 }
                                                ).ToList();
            }
            try
            {
                if (dt_Producto_ingreso.Rows.Count > 0) //valida si existen productos en las compras
                {
                    //busqueda del indice del producto que se quiere eliminar o disminuir su cantidad
                    indice = CarroDeCompra.lista_productos.Select((item, index) => new
                    {
                        itemname = item,
                        indexx = index,
                    }).Where(x => x.itemname.Id == id_producto)
                    .First()
                    .indexx;
                    Console.WriteLine("indice encontrado" + indice);

                    int precio;
                    precio = CarroDeCompra.lista_productos[indice].precio; //obtencion de precio del producto para la actualizacion den la base de datos
                    int cantidad_total = CarroDeCompra.lista_productos[indice].cantidad - cantidad; //obencion de nueva cantidad que se ingresara para actualizar la base de datos

                    if (cantidad_total > 0 ) //valida si la cantidad es mayor a 0 y si no elimina el producto del carro de compras
                    {
                        CarroDeCompra.lista_productos[indice].cantidad = cantidad_total;
                    Console.WriteLine(CarroDeCompra.lista_productos[indice].cantidad);
                    //actualizacion de precios y cantidades en el carro de compras
                    string QueryActualizacionPorductos = "begin tran UPDATE detalle_compra set sub_total = " + precio * CarroDeCompra.lista_productos[indice].cantidad + ", cantidad = " + CarroDeCompra.lista_productos[indice].cantidad + " where id_compra = " + CarroDeCompra.id_compra + " and id_producto = " + id_producto + " commit tran";
                    DataTable dt = db.Execute(QueryActualizacionPorductos);
                    string QueryActualizacionCompra = "begin tran UPDATE COMPRA set descuento = " + CarroDeCompra.Descuento + " , total = " + CarroDeCompra.Total + " where id_compra = " + CarroDeCompra.id_compra + " commit tran;";
                    db.Execute(QueryActualizacionCompra);
                    }
                    else//en el caso de que la cantidad sea menor que cero esta eliminara los productos del carro de compra
                    {
                        string query = "begin tran delete from detalle_compra where id_compra = " + CarroDeCompra.id_compra + " and id_producto = " + id_producto + " commit tran";
                        db.Execute(query);
                        CarroDeCompra.lista_productos.RemoveAt(indice);
                        string QueryActualizacionCompra = "begin tran UPDATE COMPRA set descuento = " + CarroDeCompra.Descuento + " , total = " + CarroDeCompra.Total + " where id_compra = " + CarroDeCompra.id_compra + " commit tran;";
                        db.Execute(QueryActualizacionCompra);
                    }
                }
            }catch (Exception ex) //en caso de no existir el producto no eliminara nada
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("no existe el producto en la compra");


            }



        }
        public void ConfirmarCompra()//metodo confirma el carro de compra como una compra realizada 
        {
            //ingreso de fechas para actualizar la tabla delivey en la base de datos
            DateTime fechaActual = DateTime.Now;
            DateTime fechaEntrega = fechaActual.AddDays(3);
            //formateo de fecha para entregarla a la base de datos 
            fechaActual.ToString("dd/MM/yyyy");
            fechaEntrega.ToString("dd/MM/yyyy");
            //validador de que el usuario tenga sus datos bien ingresado para que el delivery se pueda realizar
            if(CarroDeCompra.id_compra != 0 & UsuarioServicio.UsuarioIniciado.Direccion != "" & UsuarioServicio.UsuarioIniciado.Id != 0 & CarroDeCompra.lista_productos.Count > 0 & UsuarioServicio.UsuarioIniciado.Comuna != "" & UsuarioServicio.UsuarioIniciado.Email != "" & UsuarioServicio.UsuarioIniciado.telefono != "")
            {
                //Querys de ingreso nueva compra en el delivery y actualizacion de el estado de la compra 
                string QueryIngresoDelivery = "BEGIN TRAN Insert into delivery values (NEXT VALUE FOR SQ_id_delivery, "+CarroDeCompra.id_compra +", 'Diego Diaz','"+UsuarioServicio.UsuarioIniciado.Direccion+"',convert(date , ' "+ fechaActual.ToString("dd/MM/yyyy")+ " ',103), convert(date , ' "+fechaEntrega.ToString("dd/MM/yyyy")+ " ',103)) COMMIT TRAN";
                string QueryActulizacionCompra = "begin tran UPDATE compra set id_estado_compra = 2 where id_compra = "+CarroDeCompra.id_compra +" commit tran";
                db.Execute(QueryIngresoDelivery);
                db.Execute(QueryActulizacionCompra);
                //una vez que se ejecute este paso el carro de compras quedara vacio 
                CarroDeCompra = new compras();
            }
            else
            {
                if (CarroDeCompra.id_compra != 0)
                {
                    Console.WriteLine("No hay compra registrada");
                }
                else if (UsuarioServicio.UsuarioIniciado.Direccion != "")
                {
                    Console.WriteLine("No tiene direccion registrada");
                } else if (UsuarioServicio.UsuarioIniciado.Id != 0)
                {
                    Console.WriteLine("No hay usuario registrado a la compra");
                } else if (CarroDeCompra.lista_productos.Count > 0)
                {
                    Console.WriteLine("No hay productos asociadoa al carro de compra");
                } else if (UsuarioServicio.UsuarioIniciado.Comuna != "" || UsuarioServicio.UsuarioIniciado.Email != "" || UsuarioServicio.UsuarioIniciado.telefono != "")
                {
                    Console.WriteLine("La informacion de usuario es incorrecta");
                }
            }
        }
        public void IngresoCupon(string codigo_cupon) //metodo que ingresa un cupon a la compra seleccionada
        {
            try 
            { 
                //query que valida que exista el cupon
                string Query = "select id_cupon as id_cupon, estado as estado, nombre as nombre, codigo as codigo, cant_uso as cantidad, convert(varchar,fecha_compra,103) as fecha_compra, convert(varchar,fecha_entrega,103) as fecha_entrega from cupon where codigo = '" + codigo_cupon+"'";
                DataTable dt = new DataTable();
                dt = db.Execute(Query);
                CultureInfo provider = new CultureInfo("es-CL");
                DateTime fecha_expira = DateTime.ParseExact(dt.Rows[0]["fecha_entrega"].ToString(), "dd/MM/yyyy", provider);
                int cantidad_uso;
                //validador de que el cupon se encuentre activo 
                if (dt.Rows[0]["estado"].ToString() == "T" & fecha_expira > DateTime.Now & Convert.ToInt32(dt.Rows[0]["cantidad"]) > 0)
                {
                    CarroDeCompra.cupon = new Cupon //ingreso de la informacion del cupon para el carro de compras
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["id_cupon"]),
                        Nombre = dt.Rows[0]["nombre"].ToString(),
                        Codigo = dt.Rows[0]["codigo"].ToString(),
                        CantidadDesuento = Convert.ToInt32(dt.Rows[0]["cantidad"]),
                        FechaInicio = fecha_expira,
                        FechaTermino = DateTime.ParseExact(dt.Rows[0]["fecha_entrega"].ToString(), "dd/MM/yyyy", provider) 
                    };
                    int descuento = CarroDeCompra.Descuento;
                    cantidad_uso =Convert.ToInt32(dt.Rows[0]["cantidad"]) -1;
                    //actulizacion de la compra y del detalle de la compra en la base de datos
                    string QueryIngresoCupon = "begin tran update compra set id_cupon = " + CarroDeCompra.cupon.Id + " where id_compra = " + CarroDeCompra.id_compra+" commit tran";
                    db.Execute(QueryIngresoCupon);
                    string ingreso_desciento = "begin tran update compra set descuento = " + descuento + " where id_compra = " + CarroDeCompra.id_compra + " commit tran";
                    db.Execute(ingreso_desciento);
                    string QueryActualizacionCompra = "begin tran update cupon set cant_uso = "+ cantidad_uso + " where id_cupon = "+ Convert.ToInt32(dt.Rows[0]["id_cupon"]) + " commit tran";
                    db.Execute(QueryActualizacionCompra);
                }
            }catch (Exception ex)//en el caso de no ser valido en cupon no ingresara nada
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Cupon no valido");
            }
        }

        public compras BusquedaCarroCompras()//metodo que consulta el carro de compra del usuario que se encutre actuvo
        {
            //variables que rellenan informacion del carro de compras
            IList<Productos> ListaProductos = new List<Productos>();
            compras CompraIndividual = new compras();
            CompraIndividual.lista_productos = ListaProductos;
            //Query que consulta por las compras que tiene el cliente iniciado
            String Query = String.Format("select dp.cantidad as cantidad_producto, isnull(Convert(varchar,cu.fecha_compra,103),Convert(varchar,sysdatetime(),103)) as fecha_termino, isnull(Convert(varchar,cu.fecha_entrega,103),Convert(varchar,sysdatetime(),103)) as fecha_inicio, isnull(cu.cant_uso, 0) as cantidad_uso, isnull(cu.codigo, 'Sin codigo') as condigo_desc, isnull(cu.cant_descuento,0) as descuento_cupon, isnull(cu.nombre,'Sin cupon') as nombre_cupon, isnull(cu.id_cupon,0) as id_cupon, c.id_compra as id_compra, c.id_estado_compra as id_estado_compra, p.id_producto as id_producto, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) LEFT JOIN cupon cu on (c.id_cupon = cu.id_cupon) where  c.id_estado_compra = " + 1 +" and c.id_usuario = "+ UsuarioServicio.UsuarioIniciado.Id);
            DataTable dt = db.Execute(Query);
            if (dt.Rows.Count > 0) //validador para que saber si el usuario tiene un carro de compras 
            {
                CompraIndividual.id_compra = Convert.ToInt32(dt.Rows[0]["id_compra"]);
                CompraIndividual.id_usuario = Convert.ToInt32(dt.Rows[0]["id_usuario"]);
                foreach (DataRow dr in dt.Rows)
                {
                    ListaProductos.Add(new Productos
                    {
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

                CompraIndividual.Estado_compra = (EstadoCompra)Convert.ToInt32(dt.Rows[0]["id_estado_compra"]);
            }
            //actulizacion del carro de compras
            CarroDeCompra = CompraIndividual;
            return CompraIndividual;
        }
    }
}

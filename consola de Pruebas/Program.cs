using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;

api_proyecto_web.DBConText.Connection db = new api_proyecto_web.DBConText.Connection();
string Query = "select id_producto as id_producto, nombre as nombre, caracteristicas as caracteristicas, id_tipo_producto as tipo  from producto where id_producto = " + 2;
DataTable dt = db.Execute(Query);


Productos productos = new Productos { Id = Convert.ToInt32(dt.Rows[0]["id_producto"]), nombre = dt.Rows[0]["nombre"].ToString(), caracteristicas = dt.Rows[0]["caracteristicas"].ToString()};

productos.tipo_producto = (Tipo_Producto)43;


Console.WriteLine("id: "+productos.precio);
Console.WriteLine("nombre: " + productos.nombre);
Console.WriteLine("caracteristicas: " + productos.caracteristicas);
Console.WriteLine("tipo_producto: " + productos.tipo_producto);

using api_proyecto_web.DBConText;
using Oracle.ManagedDataAccess.Client;
using System.Data;

api_proyecto_web.DBConText.Connection db = new api_proyecto_web.DBConText.Connection();

db = new api_proyecto_web.DBConText.Connection("User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;");


string Query = string.Format(@"select c.id_compra as id_compra, c.id_usuario as id_usuario, p.id_tipo_producto as id_tipo_producto, p.nombre as nombre_producto, p.caracteristicas as caracteristicas, p.precio as precio from compra c join detalle_compra dp on (dp.id_compra = c.id_compra) join producto p on (p.id_producto = dp.id_producto) where  c.id_compra = " + 1);

DataTable dt = db.Execute(Query);

string nombre = (string)dt.Rows[0]["nombre_producto"];
Console.WriteLine(nombre);
Console.WriteLine("----------");
foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine(dr["nombre_producto"]);
}



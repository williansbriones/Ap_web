using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using Oracle.ManagedDataAccess.Client;
using System.Data;


Connection con = new Connection();

string Query = "select * from producto";

DataTable dt;

dt = con.Execute(Query);


Console.WriteLine("cantidad de filas: "+dt.Rows.Count);

Console.WriteLine("segundo producto es:"+ dt.Rows[1]["nombre"]);



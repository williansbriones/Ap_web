using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Drawing.Printing;

Connection db = new Connection();

var variable = new DataTable();

string query = "SELECT NEXT VALUE FOR SQ_id_usuario as numero";

variable = db.Execute(query);

Console.WriteLine(variable.Rows[0]["numero"]);
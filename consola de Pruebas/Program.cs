
using api_proyecto_web.DBConText;
using System.Data;

api_proyecto_web.DBConText.Connection db = new api_proyecto_web.DBConText.Connection();

db = new api_proyecto_web.DBConText.Connection("User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;");


string Query = string.Format(@"select id_estado_compra, nombre from estado_compra");

DataTable dt = db.Execute(Query);

Console.WriteLine(dt.Rows.ToString());



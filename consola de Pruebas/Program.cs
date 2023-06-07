
//using api_proyecto_web.DBConText;
//using System.Data;

//api_proyecto_web.DBConText.Connection db = new api_proyecto_web.DBConText.Connection();

//db = new api_proyecto_web.DBConText.Connection("User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
//                          + "Connection Timeout=30;");


//string Query = string.Format(@"select id_estado_compra, nombre from estado_compra");

//DataTable dt = db.Execute(Query);
//Console.WriteLine(dt.Columns);
//Console.WriteLine(dt);
//Console.WriteLine("Select ejecutado");

using Oracle.ManagedDataAccess.Client;


string ConString = "User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;";


OracleConfiguration.TnsAdmin = "C:\\Users\\willi\\Desktop\\Api_web\\consola de Pruebas\\Wallet\\";
OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;

OracleConnection Conection = new OracleConnection(ConString);
OracleCommand command = Conection.CreateCommand();

Conection.Open();

command.CommandText = "select id_estado_compra, nombre from estado_compra";
OracleDataReader reader = command.ExecuteReader();
while (reader.Read())
{

    Console.WriteLine(reader.GetInt32(0) + " - " + reader.GetString(1));
}

Console.WriteLine("la base de datos esta activa");

Conection.Close();

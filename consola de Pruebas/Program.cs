// See https://aka.ms/new-console-template for more information
using api_proyecto_web.Modelos;
using static System.Net.Mime.MediaTypeNames;

using Oracle.ManagedDataAccess.Client;

string conString = "User Id=ADMIN;Password=ProgramacionWeb2023#; Data Source=n4jrb5b37b3i2h3f_high;"
    + "connection timeout=30;";


OracleConfiguration.TnsAdmin = "C:\\Users\\willi\\Desktop\\Api_web\\consola de Pruebas\\Wallet\\";
OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;


OracleConnection connection = new OracleConnection(conString);
OracleCommand command = connection.CreateCommand();

connection.Open();


command.CommandText = "select * from usuario";

Console.WriteLine("la base de datos a sido actualizada");


connection.Close();





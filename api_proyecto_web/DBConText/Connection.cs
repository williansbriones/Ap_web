using System.Data;
using System.Data.SqlClient;

namespace api_proyecto_web.DBConText
{
    public class Connection
    {
        private string connection_str = string.Empty;
        public Connection()
        {
            var constructor = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            connection_str = constructor.GetSection("ConnectionStrings:azure").Value;
        }

        public  DataTable Execute(string query, Dictionary<string, object> parametros)
        {
            using(var sql = new SqlConnection(connection_str))
            {
                try 
                { 
                    using(var cmd = new SqlCommand(query, sql))
                    {
                        sql.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(reader);
                        sql.CloseAsync();
                        Console.WriteLine("coneccion exitosa");
                        return dt;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new DataTable();
                }

            }


              
        }

    }
}

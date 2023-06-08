using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace api_proyecto_web.DBConText
{
    public class Connection
    {
        string ConnectionString = "User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high; " + " Connection Timeout=30;";
        public Connection()
        {
            OracleConfiguration.TnsAdmin = @"Wallet";
            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
        }
        public Connection(string conn)
        {
            this.ConnectionString = conn;
        }
        public DataTable Execute(string SQL)
        {
            using (OracleConnection con = new OracleConnection(this.ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        Console.WriteLine("Base de datos connectada con exito");
                        Console.WriteLine();

                        cmd.CommandText = SQL;
                        OracleDataReader reader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(reader);
                        con.Close();
                        Console.WriteLine("Select ejecutado");
                        Console.WriteLine("Cantidad de lineas consultadas: " + dt.Rows.Count);
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("No se vinculo la con la base de datos");
                    }
                    con.Close();
                    return new DataTable();
                }
            }
        }

        public void ExecuteInsert(string SQL)
        {
            using (OracleConnection con = new OracleConnection(this.ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        Console.WriteLine("Base de datos connectada con exito");
                        Console.WriteLine();

                        cmd.CommandText = SQL;
                        OracleDataReader reader = cmd.ExecuteReader();
                        con.Close();
                        Console.WriteLine("wena choro eri weno");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("No se vinculo la con la base de datos");
                    }
                    con.Close();
                }
            }

        }
    }
}

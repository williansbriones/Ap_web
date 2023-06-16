using api_proyecto_web.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class CuponServicios : IcrudCupon
    {
        //Coneccion BD
        static DBConText.Connection db = new DBConText.Connection();
        private object delete;

        public CuponServicios()
        {
            db = new DBConText.Connection();
        }
        //credenciales para realizar la consulta en la base de datos
        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            String query = "insert  into cupon VALUES (SQ_IDcupon.NEXTVAL,'T',"+CantidadDesuento+",'" + Nombre + "','"+ Codigo+"',"+ Cantidad_limite+",'" + FechaInicio+"','"+ FechaTermino + "')";
            DataTable dt = db.Execute(query);

            dt = db.Execute("commit");
        }
        ///-----------------------------------------------------------
        public void Des_Habilitar(int id_cupon, bool habilitar)
        {
            using (SqlConnection connection = new SqlConnection("TuCadenaDeConexion"))
            {
                string query = "UPDATE cupon SET Estado = @Estado WHERE id_cupon = @id_cupon";
                string estado = habilitar ? "T" : "F";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Estado", estado);
                command.Parameters.AddWithValue("@id_cupon", id_cupon);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //-------------------------------------------------------------
        public void Eliminarcupon(int id_cupon)
        {
            using (SqlConnection connection = new SqlConnection("TuCadenaDeConexion"))
            {
                string query = "DELETE FROM cupon WHERE id_cupon = @id_cupon";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id_cupon", id_cupon);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------
        public void Modificarcupon(int id_cupon, string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            using (SqlConnection connection = new SqlConnection("TuCadenaDeConexion"))
            {
                string query = "UPDATE cupon SET Nombre = @Nombre, CantidadDesuento = @CantidadDesuento, Codigo = @Codigo, Cantidad_limite = @Cantidad_limite, FechaInicio = @FechaInicio, FechaTermino = @FechaTermino WHERE id_cupon = @id_cupon";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@CantidadDesuento", CantidadDesuento);
                command.Parameters.AddWithValue("@Codigo", Codigo);
                command.Parameters.AddWithValue("@Cantidad_limite", Cantidad_limite);
                command.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                command.Parameters.AddWithValue("@FechaTermino", FechaTermino);
                command.Parameters.AddWithValue("@id_cupon", id_cupon);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // reemplazar "TuCadenaDeConexion"
        //SqlCommand para ejecutar las consultas. remplazar 

    }
}

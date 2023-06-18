using api_proyecto_web.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class CuponServicios : IcrudCupon
    {
        // Conección BD
        static DBConText.Connection db = new DBConText.Connection();
        private object delete;

        public CuponServicios()
        {
            db = new DBConText.Connection();
        }

        // Credenciales para realizar la consulta en la base de datos
        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            string query = "insert into cupon VALUES (NextCuponID, 'T', @CantidadDesuento, @Nombre, @Codigo, @Cantidad_limite, @FechaInicio, @FechaTermino)";
            var parametros = new
            {
                CantidadDesuento,
                Nombre,
                Codigo,
                Cantidad_limite,
                FechaInicio,
                FechaTermino
            };
            DataTable dt = db.Execute(query, parametros);

            dt = db.Execute("commit");
        }

        public void Des_Habilitar(int id_cupon, bool habilitar)
        {
            string estado = habilitar ? "T" : "F";
            string query = "UPDATE cupon SET Estado = @Estado WHERE id_cupon = @id_cupon";

            var parametros = new { Estado = estado, id_cupon };
            DataTable dt = db.Execute(query, parametros);

            dt = db.Execute("commit");
        }

        public void Eliminarcupon(int id_cupon)
        {
            string query = "DELETE FROM cupon WHERE id_cupon = @id_cupon";
            var parametros = new { id_cupon };
            DataTable dt = db.Execute(query, parametros);

            dt = db.Execute("commit");
        }

        public void Modificarcupon(int id_cupon, string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            string query = "UPDATE cupon SET Nombre = @Nombre, CantidadDesuento = @CantidadDesuento, Codigo = @Codigo, Cantidad_limite = @Cantidad_limite, FechaInicio = @FechaInicio, FechaTermino = @FechaTermino WHERE id_cupon = @id_cupon";

            var parametros = new
            {
                Nombre,
                CantidadDesuento,
                Codigo,
                Cantidad_limite,
                FechaInicio,
                FechaTermino,
                id_cupon
            };

            DataTable dt = db.Execute(query, parametros);

            dt = db.Execute("commit");
        }
    }
}

using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class CuponServicios : IcrudCupon
    {
        Connection db = new Connection();

        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            string query = " BEGIN TRAN insert into cupon values (next value for SQ_IDcupon,'T'," + CantidadDesuento + ",'" + Nombre + "','" + Codigo + "'," + Cantidad_limite + ", CONVERT(DATE, '" + FechaInicio + "',103),CONVERT(DATE, '" + FechaTermino + "',103)) COMMIT TRAN";
            db.Execute(query);
        }

        public void Des_Habilitar(int id_cupon)
        {
            string query = " BEGIN TRAN update cupon set estado = 'F' WHERE  id_cupon = " + id_cupon + "commit tran";
            db.Execute(query);
        }

        public void Eliminar_cupon(int id_cupon)
        {
            string query = "  BEGIN TRAN DELETE FROM cupon WHERE id_cupon = " + id_cupon + "commit tran";
            db.Execute(query);
        }

        public void Habilitar_cupon(int id_cupon)
        {
            string query = " BEGIN TRAN update cupon set estado = 'F' WHERE  id_cupon = " + id_cupon + "commit tran";
            db.Execute(query);
        }
        public void Modificarcupon(int id_cupon, string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite)
        {
            string query = " BEGIN TRAN UPDATE cupon SET cupon = '" + Nombre + "'," + CantidadDesuento + ",'" + Codigo + "'," + Cantidad_limite + " COMMIT TRAN ";

            db.Execute(query);
        }

    }
}

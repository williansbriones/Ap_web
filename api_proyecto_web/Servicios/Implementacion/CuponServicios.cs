using System.Data;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class CuponServicios : IcrudCupon
    {
        //Coneccion BD
        static DBConText.Connection db = new DBConText.Connection();
        public CuponServicios()
        {
            db = new DBConText.Connection("User Id=ADMIN;Password=ProgramacionWeb2023#;Data Source=r7dbt8zx2wqrpwgt_high;"
                          + "Connection Timeout=30;");
        }

        //credenciales para realizar la consulta en la base de datos
        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino)
        {
            String query = "insert  into cupon VALUES (SQ_IDcupon.NEXTVAL,'T',"+CantidadDesuento+",'" + Nombre + "','"+ Codigo+"',"+ Cantidad_limite+",'" + FechaInicio+"','"+ FechaTermino + "')";
            DataTable dt = db.Execute(query);

            dt = db.Execute("commit");
        }

        public void Eliminarcupon(int id_cupon)
        {
            throw new NotImplementedException();
        }
    }
}

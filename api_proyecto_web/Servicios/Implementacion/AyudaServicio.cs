using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System.Data;


namespace api_proyecto_web.Servicios.Implementacion
{
    public class AyudaServicio : IcrudAyuda
    {
        static DBConText.Connection db = new DBConText.Connection();
        static Ayuda Ayuda1 = new Ayuda();

        public void GenerarAyuda(string email, string descripcion)
        {
            Ayuda ayudita = new Ayuda();
            string Query = string.Format("BEGIN TRAN insert into ayuda values (next value for id_ayuda,'"+email+"','"+descripcion+"',convert(date,'"+ DateTime.Now.ToString("dd/MM/yyyy") +"',103)) COMMIT TRAN");
            DataTable dt1 = db.Execute(Query);
  

            if (dt1.Rows.Count > 0)
            {
                ayudita.email = dt1.Rows[0]["correo"].ToString();
                ayudita.descripcion = dt1.Rows[0]["Descripcion"].ToString();
                ayudita.fecha_ingreso = dt1.Rows[0]["fecha_ingreso"].ToString();
                
                
            }
        }


            
        }


        
    }





using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System.Data;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class UsuarioServicio : IcrudUsuario
    {

        static DBConText.Connection db = new DBConText.Connection();
        public UsuarioServicio()
        {
            db = new Connection();
        }//credenciales para realizar la consulta en la base de datos


        public static Usuario UsuarioIniciado = UsuarioIniciado !=null ? UsuarioIniciado : new Usuario() ;
        public void CambiarContraseña(string contraseña_antigua, string Contraseña)
        {
            string query1 = string.Format("select * FROM usuario where email ='"+UsuarioIniciado.Email+"'and contraseña ='"+contraseña_antigua+"'");
            DataTable dt1 = db.Execute(query1);
            if (dt1.Rows.Count > 0)
            {
                string Query2 = @"Update Usuario set contraseña ='"+Contraseña+"' where email = '"+UsuarioIniciado.Email+ "'";
                DataTable dt2 = db.Execute(Query2);
                dt2 = db.Execute("COMMIT");
                UsuarioIniciado.Contraseña = Contraseña;
            }
        }

        public Usuario informacionUsuario()
        {
            return UsuarioIniciado;
        }

        public void InicioSesion(string correo, string contraseña)
        {
            Usuario Datos = new Usuario();
            string Query = string.Format("SELECT id_usuario AS id_usuario, nombre AS nombre , appaterno+' '+apmaterno AS apellidos, id_tipo_usuario as tipo_usuario, telefono as telefono, email as email, direccion as direccion, comuna as comuna, contraseña as contraseña FROM usuario where email ='"+correo +"'and contraseña ='"+contraseña+"'");
            DataTable dt1 = db.Execute(Query);

            if(dt1.Rows.Count > 0)
            {
                Datos.Id            = Convert.ToInt32(dt1.Rows[0]["id_usuario"]);
                Datos.Nombre        = dt1.Rows[0]["nombre"].ToString();
                Datos.Apellido      = dt1.Rows[0]["apellidos"].ToString();
                Datos.tipo_Usuario  = (Tipo_usuario)Convert.ToInt32(dt1.Rows[0]["tipo_usuario"]);
                Datos.telefono      = dt1.Rows[0]["telefono"].ToString();
                Datos.Email         = dt1.Rows[0]["email"].ToString();
                Datos.Direccion     = dt1.Rows[0]["direccion"].ToString();
                Datos.Comuna        = dt1.Rows[0]["comuna"].ToString();
                Datos.Contraseña    = dt1.Rows[0]["contraseña"].ToString();
            }


            UsuarioIniciado = Datos;
        }
    }
}

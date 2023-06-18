using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using Newtonsoft.Json.Linq;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace api_proyecto_web.Servicios.Implementacion
{
    public class UsuarioServicio : IcrudUsuario
    {

        static DBConText.Connection db = new DBConText.Connection();



        public static Usuario UsuarioIniciado = UsuarioIniciado !=null ? UsuarioIniciado : new Usuario() ;
        public void CambiarContraseña(string contraseña_antigua, string Contraseña) //Metodo que cambia la contraseña del usuario que se encuentre iniciado
        {
            //Query que valida si existe un usuario con la el corrreo y la contraseña señalada
            string query1 = string.Format("select * FROM usuario where email ='"+UsuarioIniciado.Email+"'and contraseña ='"+contraseña_antigua+"'");
            DataTable dt1 = db.Execute(query1);
            if (dt1.Rows.Count > 0) //valida que exista registro  en la base de datos
            {
                //Actualizacion de contraseña en la base de datos
                string Query2 = "begin tran Update Usuario set contraseña ='"+Contraseña+"' where email = '"+UsuarioIniciado.Email+ "' commit tran";
                DataTable dt2 = db.Execute(Query2);
                UsuarioIniciado.Contraseña = Contraseña;
            }
        }

        public Usuario informacionUsuario()//metodo que entrega la informacion del usuario que se encuentre iniciado 
        {
            return UsuarioIniciado;
        }

        public void InicioSesion(string correo, string contraseña) //metodod que inicia sesion del usuario
        {
            Usuario Datos = new Usuario();//variable que entregara los datos al usuario iniciado
            //quey que valida si existe algun usuario con la contraseña y correo ingresados
            string Query = string.Format("SELECT id_usuario AS id_usuario, nombre AS nombre , appaterno+' '+apmaterno AS apellidos, id_tipo_usuario as tipo_usuario, telefono as telefono, email as email, direccion as direccion, comuna as comuna, contraseña as contraseña FROM usuario where email ='"+correo +"'and contraseña ='"+contraseña+"'");
            DataTable dt1 = db.Execute(Query);
            if(dt1.Rows.Count > 0)//validador de que exista informacion
            {
                //ingreso de informacion en el objeto datos
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

            //ingreso de informacion en el usuario iniciado
            UsuarioIniciado = Datos;
        }

        public void CrearUsuario(string nombre, string apellidos, string telefono, string email, string direccion, string comuna, string contraseña) // metodo que permite crear un usuario
        {
            //variables que perimitiran saber si tiene uno o dos apellidos
            string primer_apellido = "";
            string segundo_apellido = "";
            int indice_espacio = (apellidos.IndexOf(" "));
            int largoApellido = (apellidos.Length);

            if (indice_espacio > 0)//validador para saber si tiene uno o dos apellidos
            {
                primer_apellido = apellidos.Substring(0, (indice_espacio - 1));
                segundo_apellido = apellidos.Substring((indice_espacio + 1), (largoApellido - (indice_espacio + 1)));
            }
            else//en el caso de tener un apellido setea el apellido paterno 
            {
                primer_apellido = apellidos;
            }
            //ingreso de datos del usuario registrado a la base de datos
            string QueryCreacionUsuario = "begin tran INSERT INTO usuario VALUES (NEXT VALUE FOR SQ_id_usuario, '" + nombre + "' ,'" + primer_apellido + "','" + segundo_apellido + "',1,'" + telefono + "','" + email + "','" + direccion + "','" + comuna + "','" + contraseña + "') COMMIT TRAN";
            db.Execute(QueryCreacionUsuario);
        }
        public void EditarUsuario(string nombre, string apellido, string telefono, string email, string direccion, string comuna)
        {
            if(UsuarioIniciado.Id == 0) //codigo que genera un usuario el cual se incia en caso de no tener un usuario iniciado
            {
                string query = "SELECT NEXT VALUE FOR SQ_id_usuario as numero";
                DataTable dt_id_nuevo_usuario = new DataTable();
                dt_id_nuevo_usuario = db.Execute(query);
                string query_ingreso_usuario = "begin tran INSERT INTO usuario VALUES("+ dt_id_nuevo_usuario.Rows[0]["numero"] +", '', '', '', 0, '"+dt_id_nuevo_usuario.Rows[0]["numero"]+"', '"+ dt_id_nuevo_usuario.Rows[0]["numero"] + "', '', '', '') COMMIT TRAN";
                db.Execute(query_ingreso_usuario);
                UsuarioIniciado.Id = Convert.ToInt32(dt_id_nuevo_usuario.Rows[0]["numero"]);
            }
            //edita la informacion del usuaria en caso de que se le indique en algun parametro
            string nombre_str = nombre != ""? nombre : UsuarioIniciado.Nombre;
            string apellido_str = apellido != ""? apellido : UsuarioIniciado.Apellido;
            string telefono_str = telefono != ""? telefono : UsuarioIniciado.telefono;
            string email_str = email != ""? email : UsuarioIniciado.Email;
            string direccion_str = direccion != ""? direccion : UsuarioIniciado.Direccion;
            string comuna_str = comuna != ""? comuna : UsuarioIniciado.Comuna;
            string primer_apellido = "";
            string segundo_apellido = "";
            //apellidos 
            int indice_espacio = (apellido_str.IndexOf(" "));
            int largoApellido = (apellido_str.Length);
            //separa apellidos segun indique en algun caso
            if (indice_espacio > 0)
            {
                primer_apellido = apellido_str.Substring(0, (indice_espacio - 1));
                segundo_apellido = apellido_str.Substring((indice_espacio + 1), (largoApellido - (indice_espacio + 1)));
            }
            else
            {
                primer_apellido = apellido_str;
            }
            string queryValidarUsuario = "select * from usuario where email = '"+email_str+"' or telefono = '"+telefono_str+"'";
            DataTable dtValidadorUsuario = new DataTable();
            dtValidadorUsuario = db.Execute(queryValidarUsuario);
            //actualiza los datos del usuario
            if (dtValidadorUsuario.Rows.Count == 0) 
            { 
                string QueryInsert = "begin tran UPDATE usuario SET nombre= '" + nombre_str+"', appaterno = '"+primer_apellido+"', apmaterno = '"+segundo_apellido+"', telefono = '"+telefono_str+"', email= '"+email_str+"', direccion='"+direccion_str+"', comuna = '"+comuna_str+"' WHERE id_usuario = "+UsuarioIniciado.Id+ " COMMIT TRAN"; 
                UsuarioIniciado.Nombre = nombre_str;
                UsuarioIniciado.Apellido = apellido_str;
                UsuarioIniciado.telefono = telefono_str;
                UsuarioIniciado.Email = email_str;
                UsuarioIniciado.Direccion = direccion_str;
                UsuarioIniciado.Comuna = comuna_str;
                db.Execute(QueryInsert);
            }
            else
            {
                Console.WriteLine("No se ingresaron los datos de usuario ya que hay datos repetidos");
            }
        }
        public void cerrarSesion() //cierra sesion del usuario que se encuentre iniciado 
        {
            UsuarioIniciado = new Usuario();
        }
    }
}

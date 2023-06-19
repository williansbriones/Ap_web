using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using System;
using System.Data;
using System.Transactions;
namespace api_proyecto_web.Servicios.Implementacion
{

    public class ProductoServicios : IcrudProductos
    {
        static DBConText.Connection db = new DBConText.Connection();
        static Productos producto1 = new Productos();

        public ProductoServicios()
        {
            db = new Connection();
        }

        public void EliminarProducto(int id)
        {
            string query1 = string.Format("BEGIN TRAN DELETE from producto WHERE id_producto=" +id)+"COMMIT TRAN";
            DataTable dt1 = db.Execute(query1);
            dt1 = db.Execute("COMMIT");

        }
        
        

        public void GenerarProducto(int id_tipo_Producto, string nombre, string caracteristicas, int precio,bool estado )
        {
            Productos productito = new Productos();
            string query1 = string.Format("BEGIN TRAN insert into producto values (next value for id_producto,'"+ nombre + "','"+ caracteristicas + "','"+ id_tipo_Producto + "','"+precio+"','T') COMMIT TRAN");
            DataTable dt1 = db.Execute(query1);
            


            if (dt1.Rows.Count > 0)
            {
                productito.tipo_producto = (Tipo_Producto)Convert.ToInt32(dt1.Rows[0]["id_tipo_producto"]);
                productito.nombre = dt1.Rows[0]["nombre"].ToString();
                productito.caracteristicas = dt1.Rows[0]["caracteristicas"].ToString();
                productito.precio = Convert.ToInt32(precio);
                productito.estado = true;
                
                
            }
        }

        public Productos InformacionProducto(int id)
        {
            string query1 = string.Format("BEGIN TRAN Select * from producto WHERE id_producto=" + id) + "COMMIT TRAN";
            DataTable dt1 = db.Execute(query1);
            Productos produc = new Productos();
            produc.Id = Convert.ToInt32(dt1.Rows[0]["id_producto"]) ;
            produc.nombre = dt1.Rows[0]["Nombre"].ToString();
            produc.caracteristicas = dt1.Rows[0]["caracteristicas"].ToString();
            produc.precio = Convert.ToInt32(dt1.Rows[0]["id_tipo_producto"]);
            produc.estado = true;
            return produc;
            
        }

        

        
    }


    

    
}

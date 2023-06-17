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
        public static Productos ProductoCreado = ProductoCreado != null ? ProductoCreado : new Productos();

        public void EliminarProducto(int id)
        {
            string query1 = string.Format(@"UPDATE producto SET estado  = 'F' WHERE id_producto ="+id);

        }

        

        public void GenerarProducto(Tipo_Producto tipo_Producto, string nombre, string caracteristicas, int precio, Boolean estado)
        {
            Productos productito = new Productos();
            string query1 = string.Format("insert into producto values (next value for id_producto,'Nootebook MSI ','8va Generacion 3050 RTXSTUDIO','1','1200000','T')");
            DataTable dt1 = db.Execute(query1);

            if (dt1.Rows.Count > 0)
            {
                productito.tipo_producto = (Tipo_Producto)Convert.ToInt32(dt1.Rows[0]["tipo_Producto"]);
                productito.nombre = dt1.Rows[0]["nombre"].ToString();
                productito.caracteristicas = dt1.Rows[0]["caracteristicas"].ToString();
                productito.precio = Convert.ToInt32(precio);

            }
        }

        public Productos InformacionProducto(int id)
        {
            return ProductoCreado;
        }

        
    }


    

    
}

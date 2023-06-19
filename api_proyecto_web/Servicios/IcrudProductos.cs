using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Servicios
{
    public interface IcrudProductos
    {
        //POST
        public void GenerarProducto(int tipo_Producto, string nombre, string caracteristicas, int precio,Boolean estado);

        //GET 
        public Productos InformacionProducto(int id);
        
        //DELETE
        public void EliminarProducto(int Id);

        
        
    }
}

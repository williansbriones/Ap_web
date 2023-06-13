using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Servicios
{
    public interface IcrudProductos
    {
        public void GenerarProductos(Tipo_Producto tipo_Producto, string nombre, string caracteristicas, int precio
                                    , imagen imagen1, imagen imagen2, imagen imagen3, imagen imagen4, imagen imagen5, int cantidad);
    }
}

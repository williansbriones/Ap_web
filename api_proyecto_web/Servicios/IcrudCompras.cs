using api_proyecto_web.Modelos;
using System.Security.Cryptography.X509Certificates;

namespace api_proyecto_web.Servicios
{
    public interface IcrudCompras<T>
    {
        //Crud
        public IList<T> BusquedaComprasCliente(int id_cliente);
        public T BusquedaCompraIndividual(int id_compra);
        public IList<T> BusquedaComprasClienteIniciado();
        public void Agregarproducto(int id_producto, int cantidad);
        public void ConfirmarCompra();
        public void EliminarProducto(int id_producto, int cantidad);
        public void IngresoCupon(string codigo_cupon);
        public T BusquedaCarroCompras();
    }
}
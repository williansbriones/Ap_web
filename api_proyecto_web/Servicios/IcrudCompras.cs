using System.Security.Cryptography.X509Certificates;

namespace api_proyecto_web.Servicios
{
    public interface IcrudCompras<T>
    {
        //Crud
        public IList<T> BusquedaComprasCliente(int id_cliente);
        public T BusquedaCompraIndividual(int id_compra);
        
        public IList<T> BusquedaComprasClienteIniciado();


    }
}

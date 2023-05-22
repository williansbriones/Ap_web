using System.Security.Cryptography.X509Certificates;

namespace api_proyecto_web.Servicios
{
    public interface IcrudCompras<T>
    {
        //Crud
        public T individual(int id_compra);
        public IList<T> compras_cliente(int id_cliente);



    }
}

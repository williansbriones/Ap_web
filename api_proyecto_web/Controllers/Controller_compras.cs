using api_proyecto_web.Modelos;
using api_proyecto_web.Servicios;
using api_proyecto_web.Servicios.Implementacion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_proyecto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_compras : ControllerBase
    {
        IcrudCompras<compras> servicio_compras = new ComprasServicios();

        //get
        [HttpGet("OrdenesDeCompraClienteIniciado")]
        public IList<compras> ComprasDeUsuariosIniciado()
        {
            return servicio_compras.BusquedaComprasClienteIniciado();
        }
        // GET api/Ordenes_de_compra/id_compta
        [HttpGet("Ordenes_de_compra/{id_compra}")]//consulta de las ordenes de compra individuales
        public compras GetIndividual(int id_compra)
        {
            return servicio_compras.BusquedaCompraIndividual(id_compra);
        }
        //GET api/Ordenes_de_cliente/id_cliente
        [HttpGet("Ordenes_de_clientes/{id_cliente}",Name ="GetCompras")]//Consulta de ordenes de compra por cliente
        public IList<compras> GetCompras(int id_cliente)
        {
            return servicio_compras.BusquedaComprasCliente(id_cliente);
        }
        // POST api/<Controller_compras>
        [HttpPost("ingreso_Producto")]
        public void Post(int id_producto, int cantidad)
        {
            servicio_compras.Agregarproducto(id_producto, cantidad);
        }

        // PUT api/<Controller_compras>/5
        [HttpPut("(EliminarProducto)")]
        public void EliminarProducto(int id_producto, int cantidad)
        {
            servicio_compras.EliminarProducto(id_producto, cantidad);
        }

    }
}

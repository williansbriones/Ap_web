using api_proyecto_web.Servicios.Implementacion;
using api_proyecto_web.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;

namespace api_proyecto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Producto : ControllerBase
    {
        IcrudProductos servicioProducto = new ProductoServicios();
        [HttpGet("informacionProducto")]
        public Productos GetProducto(int id)
        {
            return servicioProducto.InformacionProducto(id);
        }


        [HttpPost("GenerarProducto")]
        public void PostProducto(int tipo_Producto, string nombre, string caracteristicas, int precio,Boolean estado)
        {
            servicioProducto.GenerarProducto(tipo_Producto, nombre, caracteristicas, precio,estado);
        }

    }
}


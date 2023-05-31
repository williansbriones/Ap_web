using api_proyecto_web.Modelos;
using api_proyecto_web.Modelos.@enum;
using api_proyecto_web.Servicios;
using api_proyecto_web.Servicios.Implementacion;
using UsuarioServicio = api_proyecto_web.Servicios.Implementacion.UsuarioServicio;

internal class Program
{

    public static void Main(string[] args)
    {
        
       

        Console.WriteLine("El id es "+UsuarioServicio.UsuarioIniciado.Id);


    }
}
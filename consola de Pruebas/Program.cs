// See https://aka.ms/new-console-template for more information
using api_proyecto_web.Modelos;
using static System.Net.Mime.MediaTypeNames;


 IList<Productos> ListaProducto = new List<Productos>();


if (ListaProducto.Count == 0)
{
    Console.WriteLine("esta Vacia");
}else
{
    Console.WriteLine("tiene Elementos");
}

ListaProducto.Add(new Productos
{
    Id = 2,
    nombre = "core I7",
    caracteristicas = "procesador",
    precio = 700000,
    imagen1 = new imagen(),
    imagen2 = new imagen(),
    imagen3 = new imagen(),
    imagen4 = new imagen(),
    imagen5 = new imagen(),
    Descuento = 0,
    oferta = 10,
});
ListaProducto.Add(new Productos
{
    Id = 3,
    nombre = "GTX 1250 IT",
    caracteristicas = "Tarjetas Graficas",
    precio = 1200000,
    imagen1 = new imagen(),
    imagen2 = new imagen(),
    imagen3 = new imagen(),
    imagen4 = new imagen(),
    imagen5 = new imagen(),
    Descuento = 0,
    oferta = 0,
});
ListaProducto.Add(new Productos
{
    Id = 3,
    nombre = "GTX 1550 IT",
    caracteristicas = "Tarjetas Graficas",
    precio = 1200000,
    imagen1 = new imagen(),
    imagen2 = new imagen(),
    imagen3 = new imagen(),
    imagen4 = new imagen(),
    imagen5 = new imagen(),
    Descuento = 0,
    oferta = 0,
});


ListaProducto.RemoveAt(0);
ListaProducto.RemoveAt(0);
Console.WriteLine(ListaProducto[0].nombre);

Console.WriteLine("--------------------------");
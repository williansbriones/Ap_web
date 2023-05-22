// See https://aka.ms/new-console-template for more information
using api_proyecto_web.Modelos;


IList<compras> lista = new List<compras>();
lista.Add(new compras { id_compra = 1 });
lista.Add(new compras { id_compra = 2 });


foreach (compras com in lista)
{
    Console.WriteLine(com.id_compra);
}


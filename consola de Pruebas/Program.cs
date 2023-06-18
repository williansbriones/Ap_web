using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;

CultureInfo provider = new CultureInfo("es-CL");

DateTime fecha = DateTime.ParseExact("19/06/2023","dd/MM/yyyy", provider);

Console.WriteLine(DateTime.Compare(DateTime.Now,fecha));

if (fecha > DateTime.Now)
{
    Console.WriteLine("si");
}
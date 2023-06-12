
using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using Oracle.ManagedDataAccess.Client;
using System.Data;

string fecha = "00/00/00";

DateTime fe = new DateTime();

fe = DateTime.Parse(fecha == "00/00/00" ? "21/30/21" : fecha);

Console.WriteLine(fe);
Console.WriteLine("hola");
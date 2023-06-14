
using api_proyecto_web.DBConText;
using api_proyecto_web.Modelos;
using Oracle.ManagedDataAccess.Client;
using System.Data;

IList<int> list = new List<int>();

list.Add(1);
list.Add(0);
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);
list.Add(5);
int firstItem;
try
{

    firstItem = list.Select((item, index) => new
    {
        ItemName = item,
        Position = index
    }).Where(i => i.ItemName == 0)
     .First()
     .Position;
}
catch
{
    firstItem = 0;
}


Console.WriteLine(firstItem);

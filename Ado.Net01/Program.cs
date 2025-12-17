using Ado.Net2;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");



Orm dbcontext = new Orm();
#region Insert

var data = new Dictionary<string, object>
{
        {"Name", "Fuad"},
        { "Surname", "Agazade" }
};

dbcontext.InsertData("Students", data);
#endregion



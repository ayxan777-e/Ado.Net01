using Ado.Net2;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");



Orm dbcontext = new Orm();
//#region Insert

//var data = new Dictionary<string, object>
//{
//        {"Name", "Fuad"},
//        { "Surname", "Agazade" }
//};

//dbcontext.InsertData("Students", data);
//#endregion

var data = new Dictionary<string, object>
{
    { "Name", "Fuad" },
    { "Surname", "Agazade" }
};


var where = new Dictionary<string, object>
{
    { "Id", 1 }
};


dbcontext.UpdateData("Students", data, "Id = @Id", where);

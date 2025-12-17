using Ado.Net2;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks.Dataflow;

Console.WriteLine("Hello, World!");



Orm dbcontext = new Orm();
#region Insert

//var data = new Dictionary<string, object>
//{
//        {"Name", "Fuad"},
//        { "Surname", "Agazade" }
//};

//dbcontext.InsertData("Students", data);
#endregion

#region Update

//var data = new Dictionary<string, object>
//{
//    { "Name", "Fuad" },
//    { "Surname", "Agazade" }
//};


//var where = new Dictionary<string, object>
//{
//    { "Id", 1 }
//};

#endregion


#region Delete

//var db = new Orm();



//db.Delete(
//    "Students",
//    "Id=@Id",
//    new Dictionary<string, object>
//    {
//                { "Id", 2 }
//    }
//);


//db.Delete("dbo.Teachers1");

#endregion

#region SearchData

var db = new Orm();

var results = db.Search("Students", "Name", "Fuad");

foreach (var row in results)
{
    Console.WriteLine($"{row["Id"]} - {row["Name"]} {row["Surname"]}");
}
#endregion
using Microsoft.Data.SqlClient;
namespace Ado.Net2;
public class Orm
{

    private string _connectionString = "Data Source=DESKTOP-H42M0B3\\SQLEXPRESS01;Initial Catalog=DBcontext;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

    public void InsertData(string tableName, Dictionary<string, object> data)
    {
        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();

        string columns = string.Join(", ", data.Keys);
        string values = string.Join(", ", data.Keys.Select(k => "@" + k));

        string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using SqlCommand cmd = new SqlCommand(sql, conn);

        foreach (var item in data)
        {
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value ?? DBNull.Value);
        }

        cmd.ExecuteNonQuery();

    }






}

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



    public void UpdateData(string tableName, Dictionary<string, object> data, string whereClause, Dictionary<string, object> whereParams)
    {
        // Table yoxlaması (istəyə görə)
        var allowedTables = new HashSet<string> { "Students", "Teachers", "Courses" };
        if (!allowedTables.Contains(tableName))
            throw new Exception("Invalid table name");

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            string setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));

            string sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                foreach (var item in data)
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value ?? DBNull.Value);

                if (whereParams != null)
                {
                    foreach (var item in whereParams)
                    {
                        if (!data.ContainsKey(item.Key))
                        {
                            cmd.Parameters.AddWithValue("@" + item.Key, item.Value ?? DBNull.Value);

                        }
                    }
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }
    }


    public void Delete(string tableName, string whereClause = null, Dictionary<string, object> whereParams = null)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            if (string.IsNullOrWhiteSpace(whereClause))
            {
                string sql = $"DROP TABLE {tableName}";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = $"DELETE FROM {tableName} WHERE {whereClause}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (whereParams != null)
                    {
                        foreach (var p in whereParams)
                            cmd.Parameters.AddWithValue("@" + p.Key, p.Value ?? DBNull.Value);
                    }
                    cmd.ExecuteNonQuery();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }
    }


    public List<Dictionary<string, object>> Search(string tableName, string searchColumn, string searchValue)
    {
        var results = new List<Dictionary<string, object>>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string sql = $"SELECT * FROM {tableName} WHERE {searchColumn} LIKE @Search";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Search", $"%{searchValue}%");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[reader.GetName(i)] = reader.GetValue(i);
                        results.Add(row);
                    }
                }
            }
            return results;
        }
    }
}

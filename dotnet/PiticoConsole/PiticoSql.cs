using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

public delegate void DatabaseMapRowDelegate<T>(T item, IDataRecord record, int rowNum);

public class PiticoSql
{
    private readonly string _connectionString;

    public PiticoSql(string connectionString)
    {
        _connectionString = connectionString;

    }

    //C.R.U.D.

    public List<T> Query<T>(string sql) where T : class, new()
    {
        List<T> result = new List<T>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();


            SqlCommand comm = new SqlCommand(sql, conn);
            using (SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    var obj = new T();


                    foreach (var p in obj.GetType().GetProperties())
                    {


                        if (!dr.ColumnExists(p.Name))
                        {
                            continue;
                        }

                        Object valor = dr[p.Name];
                        if (valor != null && valor != DBNull.Value)
                        {
                            switch (p.PropertyType.Name)
                            {
                                case "int":
                                case "Int32":
                                case "integer":
                                    if (Int32.TryParse(valor.ToString(), out var valInt32))
                                    {
                                        p.SetValue(obj, valInt32);
                                    }
                                    break;

                                case "long":
                                case "Int64":
                                    if (Int64.TryParse(valor.ToString(), out var valInt64))
                                    {
                                        p.SetValue(obj, valInt64);
                                    }
                                    break;

                                case "DateTime":
                                    if (DateTime.TryParse(valor.ToString(), out var valDateTime))
                                    {
                                        p.SetValue(obj, valDateTime);
                                    }
                                    break;

                                case "string":
                                case "String":
                                    p.SetValue(obj, valor.ToString());
                                    break;

                                case "double":
                                case "Double":
                                    if (double.TryParse(valor.ToString(), out var valDoub))
                                    {
                                        p.SetValue(obj, valDoub);
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }


                    result.Add(obj);
                }
            }
        }

        return result;
    }




















    public IEnumerable<T> MapRows<T>(string sql, DatabaseMapRowDelegate<T> action) where T : class, new()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            int rowNum = 0;
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T item = new T();
                    action(item, reader, rowNum);
                    yield return item;
                    rowNum++;
                }
            }
        }
    }







}


public static class DataReaderExtensions
{
    public static bool ColumnExists(this IDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;


namespace PiticoOrm
{

    public static class PiticoHelpers
    {
        public static IServiceCollection AddPitico(this IServiceCollection col)
        {
            return col
                .AddScoped<IPiticoSql, PiticoSql>();
        }
    }

    public delegate void DatabaseMapRowDelegate<in T>(T item, IDataRecord record, int rowNum) where T: class, new();

    public class PiticoSql : IPiticoSql
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



        public T Add<T>(T item) where T : class, new()
        {
            string fields = "";
            string values = "";
            List<SqlParameter> parametros = new List<SqlParameter>();

            var props = item.GetType().GetProperties().Where(x => x.Name != "id" && x.GetValue(item) != null).ToList();

            fields = string.Join(",", props.Select(x => x.Name));

            values = string.Join(",", props.Select(x => "@" + x.Name));

            parametros.AddRange(
                props
                .Select(x => new SqlParameter
                (
                    "@" + x.Name,
                    x.PropertyType.Name switch
                    {
                        "Int32" or "int" => SqlDbType.Int,
                        "Int64" or "long" => SqlDbType.BigInt,
                        "DateTime" => SqlDbType.DateTime,
                        "double" or "Double" => SqlDbType.Float,
                        "string" or "String" => SqlDbType.VarChar,
                        _ => throw new ArgumentOutOfRangeException($"Propriedade {x.Name} Tipo {x.PropertyType.Name} não encontrado"),
                    }

                )
                {
                    Value = x.GetValue(item)
                })
            );


            foreach (var p in parametros)
            {
                Console.WriteLine($"{p.ParameterName}={p.Value}");
            }


            string sql = $"insert into {(typeof(T).Name)}s ({fields}) values ({values}); select scope_identity()";

            Console.WriteLine(sql);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(parametros.ToArray());

                int result = Convert.ToInt32(command.ExecuteScalar());

                return Query<T>($"select * from {(typeof(T).Name)}s where id = {result}").First();
            }
        }


        public T Update<T>(T item) where T : class, new()
        {
            int id = Convert.ToInt32(item.GetType().GetProperty("id").GetValue(item));
            //item.GetType().GetProperty("updatedAt").SetValue(item, DateTime.Now);


            string sqlBase = $"update {(typeof(T).Name)}s set";
            var props = item.GetType().GetProperties().Where(x => x.Name != "id" && x.GetValue(item) != null).ToList();

            var colunasUpdate = props.Select(x => $"{x.Name} = @{x.Name}").ToList();// NomeCampo = @NomeCampo
            string parametros = string.Join(",", colunasUpdate);
            string clausulaWhere = "where id = @id";

            string sqlTotal = $"{sqlBase} {parametros} {clausulaWhere}";

            List<SqlParameter> parametrosSql = new List<SqlParameter>();

            parametrosSql.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            parametrosSql.AddRange(
                props
                .Select(x => new SqlParameter
                (
                    "@" + x.Name,
                    x.PropertyType.Name switch
                    {
                        "Int32" or "int" => SqlDbType.Int,
                        "Int64" or "long" => SqlDbType.BigInt,
                        "DateTime" => SqlDbType.DateTime,
                        "double" or "Double" => SqlDbType.Float,
                        "string" or "String" => SqlDbType.VarChar,
                        _ => throw new ArgumentOutOfRangeException($"Propriedade {x.Name} Tipo {x.PropertyType.Name} não encontrado"),
                    }

                )
                {
                    Value = x.GetValue(item)
                })
            );



            Console.WriteLine(sqlTotal);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlTotal, connection);
                command.Parameters.AddRange(parametrosSql.ToArray());

                command.ExecuteScalar();

                return Query<T>($"select * from {(typeof(T).Name)}s where id = {id}").First();
            }


        }




        public void Delete<T>(T item)
        {
            int id = Convert.ToInt32(item.GetType().GetProperty("id").GetValue(item));
            string sqlBase = $"delete from {(typeof(T).Name)}s ";
            string clausulaWhere = "where id = @id";
            string sqlTotal = $"{sqlBase}  {clausulaWhere}";

            List<SqlParameter> parametrosSql = new List<SqlParameter>();

            parametrosSql.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

            Console.WriteLine(sqlTotal);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlTotal, connection);
                command.Parameters.AddRange(parametrosSql.ToArray());

                command.ExecuteScalar();
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

}


using Microsoft.Data.SqlClient;

namespace DeskJr.Data.Logger
{
    public class GenericDal
    {
        private readonly string _connectionString;

        public GenericDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> ExecuteQuery<T>(string query, Dictionary<string, object> parameters)
        {
            var results = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Burada T türündeki nesneyi oluşturmalısınız
                            var item = Activator.CreateInstance<T>();

                            // reader'dan verileri okuyun ve item nesnesini doldurun
                            // Örnek olarak:
                            // item.PropertyName = reader["ColumnName"].ToString();

                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }
    }

}

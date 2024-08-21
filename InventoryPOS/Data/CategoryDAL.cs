using InventoryPOS;
using InventoryPOS.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;

namespace InventoryPOS.Data
{
    public class CategoryDAL
    {
        private String _connectionString;
        public CategoryDAL(String connectionString) {
            _connectionString = connectionString;
        }

        public List<Category> GettAll()
        {
            var categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT category_id, name, description FROM categories", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new Category
                        {
                            categoryID = Convert.ToInt32(reader["category_id"]),
                            name = reader["name"].ToString(),
                            description = reader["description"].ToString()
                        };
                        categories.Add(category);
                    }
                    conn.Close();
                    return categories;
                }
            }
        }

        public bool create(Category category)
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            string query = "INSERT INTO categories (name, description, status, created_by) VALUES (@name, @description, @status, @created_by);";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@name", category.name },
                    { "@status", "ACTIVE" },
                    { "@description", category.description },
                    { "@created_by", 1 }
                };

                int rowsAffected = dw.ExecuteNonQuery(query, parameters);

                return rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Stack Trace: " + e.StackTrace);

                return false;
            }
        }

    }
}

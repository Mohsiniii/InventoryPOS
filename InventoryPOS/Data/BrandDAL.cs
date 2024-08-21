using InventoryPOS;
using InventoryPOS.Models;
using System.Data.SqlClient;

namespace InventoryPOS.Data
{
    public class BrandDAL
    {
        private String _connectionString;

        public BrandDAL(String connectionString) {
            _connectionString = connectionString;
        }

        public List<Brand> GettAll()
        {
            var brands = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT brand_id, name, description FROM brands", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var brand = new Brand
                        {
                            brandID = Convert.ToInt32(reader["brand_id"]),
                            name = reader["name"].ToString(),
                            description = reader["description"].ToString()
                        };
                        brands.Add(brand);
                    }
                    conn.Close();
                    return brands;
                }
            }
        }

        public bool create(Brand brand)
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            string query = "INSERT INTO brands (name, description, status, created_by) VALUES (@name, @description, @status, @created_by);";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@name", brand.name },
                    { "@status", "ACTIVE" },
                    { "@description", brand.description },
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

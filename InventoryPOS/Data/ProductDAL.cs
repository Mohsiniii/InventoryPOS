using InventoryPOS;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using InventoryPOS.Models;
namespace InventoryPOS.Data
{
    public class ProductDAL
    {
        private String _connectionString;
        public ProductDAL(String connectionString) {
            _connectionString = connectionString;
        }

        public void create()
        {
            //
        }

        public List<Product> GettAll()
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                SqlCommand cmd = new SqlCommand("SELECT product_id, name, category_id, brand_id FROM products", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        var product = new Product
                        {
                            productID = Convert.ToInt32(reader["product_id"]),
                            name = reader["name"].ToString(),
                            description = "",
                            categoryID = Convert.ToInt32(reader["category_id"]),
                            brandID = Convert.ToInt32(reader["brand_id"]),
                        };
                        products.Add(product);
                    }
                    conn.Close();
                    return products;
                }
            }
        }
    }
}

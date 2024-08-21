using InventoryPOS;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using InventoryPOS.Models;
using System.Data;
namespace InventoryPOS.Data
{
    public class ProductDAL
    {
        private String _connectionString;
        public ProductDAL(String connectionString) {
            _connectionString = connectionString;
        }

        public bool create(Product product)
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            string queryString = "INSERT INTO products (name, category_id, brand_id, description, status, created_by) VALUES (@name, @categoryID, @brandID, @description, @status, @userID)";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@name", product.name },
                    {"@categoryID", product.categoryID },
                    {"@brandID", product.brandID },
                    { "@description", product.description },
                    { "@status", "ACTIVE" },
                    { "@userID", 1 }
                };
                int infectedRows = dw.ExecuteNonQuery(queryString, parameters);
                return infectedRows > 0;
            }catch(Exception e)
            {
                return false;
            }
        }

        public List<Product> GetAllDS()
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            DataSet ds = dw.GetDataSet("SELECT p.product_id, p.name AS product_name, c.name AS category_name, b.name AS brand_name, p.created_at, p.created_by, p.updated_at, p.updated_by FROM products p LEFT JOIN categories c ON p.category_id = c.category_id LEFT JOIN brands b ON p.brand_id = b.brand_id;");
            DataTable dt = new DataTable();
            if(ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return null;
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

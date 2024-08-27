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

        public List<Product> GetAll()
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            DataSet ds = dw.GetDataSet("SELECT p.product_id, p.name AS product_name, p.description as product_description, p.category_id, c.name AS category_name, p.brand_id, b.name AS brand_name, p.created_at, p.created_by, p.updated_at, p.updated_by FROM products p LEFT JOIN categories c ON p.category_id = c.category_id LEFT JOIN brands b ON p.brand_id = b.brand_id;");
            DataTable dt = new DataTable();
            if(ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                List<Product> products = new List<Product>();
                foreach (DataRow row in dt.Rows)
                {
                    var product = new Product
                    {
                        productID = Convert.ToInt32(row["product_id"]),
                        name = row["product_name"].ToString(),
                        category = new Category
                        {
                            categoryID = row["category_id"] != DBNull.Value ? Convert.ToInt32(row["category_id"]) : 0,
                            name = row["category_name"] != DBNull.Value ? row["category_name"].ToString() : null,
                        },
                        brand = new Brand
                        {
                            brandID = row["brand_id"] != DBNull.Value ? Convert.ToInt32(row["brand_id"]) : 0,
                            name = row["brand_name"] != DBNull.Value ? row["brand_name"].ToString() : null,
                        },
                        description = row["product_description"].ToString(),
                    };

                    products.Add(product);
                }
                return products;
            }
            return null;
        }
    }
}

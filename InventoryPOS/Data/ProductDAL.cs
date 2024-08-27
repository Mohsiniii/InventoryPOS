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

        public bool update(Product product)
        {
            string queryString = @"
                UPDATE products
                SET name = @name,
                    category_id = @categoryID,
                    brand_id = @brandID,
                    description = @description
                WHERE product_id = @productID";

            var parameters = new Dictionary<string, object>
            {
                { "@name", product.name },
                { "@categoryID", product.categoryID },
                { "@brandID", product.brandID },
                { "@description", product.description },
                { "@productID", product.productID }
            };

            try
            {
                dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
                int rowsAffected = dw.ExecuteNonQuery(queryString, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool addVariant(int productID, string size_weight, decimal unit_price)
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            string queryString = @"
            INSERT INTO variants (product_id, size_weight, unit_price, stock, created_at, created_by) 
            VALUES (@productID, @sizeWeight, @unitPrice, @stock, @createdAt, @createdBy)";

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@productID", productID },
                    { "@sizeWeight", size_weight },
                    { "@unitPrice", unit_price },
                    { "@stock", 0 },
                    { "@createdAt", DateTime.Now },
                    { "@createdBy", 1 }
                };

                int infectedRows = dw.ExecuteNonQuery(queryString, parameters);
                return infectedRows > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Product GetByID(int id)
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);

            // Query to select the product by its ID
            string query = @"
                SELECT 
                    p.product_id, 
                    p.name AS product_name, 
                    p.description as product_description, 
                    p.category_id, 
                    c.name AS category_name, 
                    p.brand_id, 
                    b.name AS brand_name, 
                    p.created_at, 
                    p.created_by, 
                    p.updated_at, 
                    p.updated_by 
                FROM 
                    products p 
                LEFT JOIN 
                    categories c 
                ON 
                    p.category_id = c.category_id 
                LEFT JOIN 
                    brands b 
                ON 
                    p.brand_id = b.brand_id 
                WHERE 
                    p.product_id = @ProductID";

            // Pass the parameters as a dictionary
            var parameters = new Dictionary<string, object>
    {
        { "@ProductID", id }
    };

            DataSet ds = dw.GetDataSet(query, parameters);
            DataTable dt = new DataTable();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
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

                    return product;
                }
            }

            // Return null if no product is found
            return null;
        }

        public List<Product> GetAll()
        {
            dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
            string query = @"
                SELECT 
                    p.product_id, 
                    p.name AS product_name, 
                    p.description AS product_description, 
                    p.category_id, 
                    c.name AS category_name, 
                    p.brand_id, 
                    b.name AS brand_name,
                    v.size_weight, 
                    v.unit_price, 
                    v.stock
                FROM products p
                LEFT JOIN categories c ON p.category_id = c.category_id
                LEFT JOIN brands b ON p.brand_id = b.brand_id
                LEFT JOIN variants v ON p.product_id = v.product_id
                WHERE p.status = 'ACTIVE'
                ORDER BY p.product_id;";

            DataSet ds = dw.GetDataSet(query);
            DataTable dt = new DataTable();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                List<Product> products = new List<Product>();
                Product currentProduct = null;

                foreach (DataRow row in dt.Rows)
                {
                    int productId = Convert.ToInt32(row["product_id"]);

                    if (currentProduct == null || currentProduct.productID != productId)
                    {
                        currentProduct = new Product
                        {
                            productID = productId,
                            name = row["product_name"].ToString(),
                            description = row["product_description"].ToString(),
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
                            variants = new List<Variant>()
                        };

                        products.Add(currentProduct);
                    }

                    if (row["size_weight"] != DBNull.Value)
                    {
                        var variant = new Variant
                        {
                            sizeWeight = row["size_weight"].ToString(),
                            unitPrice = row["unit_price"].ToString(),
                            stock = row["stock"] != DBNull.Value ? Convert.ToDecimal(row["stock"]) : 0
                        };

                        currentProduct.variants.Add(variant);
                    }
                }

                return products;
            }

            return null;
        }

        public bool Remove(int productID)
        {
            // Define the SQL query to update the status of the product
            string queryString = "UPDATE products SET status = @status WHERE product_id = @productID";

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@status", "REMOVED" },
                    { "@productID", productID }
                };

                dbHelper.Wrapper dw = new dbHelper.Wrapper(_connectionString);
                int affectedRows = dw.ExecuteNonQuery(queryString, parameters);

                return affectedRows > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}

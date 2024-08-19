using InventoryPOS;
using System.Data.SqlClient;

namespace InventoryPOS.Data
{
    public class CategoryDAL
    {
        private String _connectionString;
        public CategoryDAL(String connectionString) {
            _connectionString = connectionString;
        }
    }
}

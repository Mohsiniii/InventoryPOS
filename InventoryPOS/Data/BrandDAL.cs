using InventoryPOS;
using System.Data.SqlClient;

namespace InventoryPOS.Data
{
    public class BrandDAL
    {
        private String _connectionString;

        public BrandDAL(String connectionString) {
            _connectionString = connectionString;
        }
    }
}

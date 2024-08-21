namespace InventoryPOS.Models
{
    public class Product : BaseModel
    {
        public int productID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryID { get; set; }
        public int brandID { get; set; }

        public List<Category> categories { get; set; }
        public List<Brand> brands { get; set; }
    }
}

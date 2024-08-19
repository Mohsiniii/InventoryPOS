namespace InventoryPOS.Models
{
    public class Category : BaseModel
    {
        public int productID { get; set; }
        public int categoryID { get; set; }
        public int brandID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}

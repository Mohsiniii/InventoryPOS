namespace InventoryPOS.Models
{
    public class Category : BaseModel
    {
        public int categoryID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}

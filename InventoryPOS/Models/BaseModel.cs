namespace InventoryPOS.Models
{
    public class BaseModel
    {
        public int createdBy {  get; }
        public int createdAt { get; }
        public int updatedBy { get; set; }
        public int updatedAt { get; set; }
    }
}

namespace Web.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public CatalogItemDto Product { get; set; }
        public Order Order { get; set; }
    }
}

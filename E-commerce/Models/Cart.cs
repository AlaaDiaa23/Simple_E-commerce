namespace E_commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public decimal Price { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="No must be greater than 1")]
        public int Quantity { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}

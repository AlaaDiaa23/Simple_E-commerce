


namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Required ...")]
        public string? Name { get; set; }   
         [Required (ErrorMessage ="Required ...")]
        public string? Description { get; set; }
        [Required(ErrorMessage ="Required....")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile  File { get; set; }
        public int CatId { get; set; }
        [ForeignKey(nameof(CatId))]
        public virtual Category Category { get; set; }


    }
}

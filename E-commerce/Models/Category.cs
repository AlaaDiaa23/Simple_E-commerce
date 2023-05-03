namespace E_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Required ...")]
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; }=string.Empty;
       
      
        public virtual ICollection<Product> Products { get; set; }
    }
}

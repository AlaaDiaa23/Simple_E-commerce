namespace E_commerce.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string ?Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Required...")]
        public string ?Email { get; set; }
        public string ?Subject { get; set; }
        public string ?Message { get; set; }

    }
}

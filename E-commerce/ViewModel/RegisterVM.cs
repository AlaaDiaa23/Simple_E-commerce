namespace E_commerce.ViewModel
{
    public class RegisterVM
    {
        public string? Id { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}

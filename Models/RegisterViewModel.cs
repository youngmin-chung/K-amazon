using System;
using System.ComponentModel.DataAnnotations;

namespace K_amazon.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 99)]
        public int Age { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "email needs valid format")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street address is required.")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [RegularExpression(@"[a-zA-z]\d[a-zA-Z]-\d[a-zA-Z]\d", ErrorMessage = "Invalid format")]
        [Required(ErrorMessage = "Enter a proper Postal Code")]
        public string Mailcode { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Credit Card is required.")]
        public string CreditcardType { get; set; }
        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }
    }
}

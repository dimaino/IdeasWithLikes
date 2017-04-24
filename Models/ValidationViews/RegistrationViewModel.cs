using System.ComponentModel.DataAnnotations;

namespace BlackBeltTest2.Models
{
    public class RegistrationViewModel : BaseEntity
    {   
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(2, ErrorMessage = "Name requires a minimum of 2 Letters")]
        [RegularExpression(@"^[a-zA-Z]+[ ]{1}[a-zA-Z]+$", ErrorMessage = "Name may only contain the letters a-z and A-Z with one space between the first and last name.")]
        public string Name {get;set;}
        [Required(ErrorMessage = "Alias is a required field.")]
        [MinLength(2, ErrorMessage = "Alias requires a minimum of 2 Letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alias may only contain the letters a-z and A-Z")]
        public string Alias {get;set;}
        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(8, ErrorMessage = "Password requires a minimum of 8 Letters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Required(ErrorMessage = "Password Confirmation is a required field.")]
        [Compare(nameof(Password), ErrorMessage = "Password and Password Confirmation must match.")]
        [DataType(DataType.Password)]
        public string Password_Confirmation {get;set;}
    }
}
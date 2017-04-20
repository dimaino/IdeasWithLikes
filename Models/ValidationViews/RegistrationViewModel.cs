using System.ComponentModel.DataAnnotations;

namespace LoginAndRegisterFinal.Models
{
    public class RegistrationViewModel : BaseEntity
    {   
        [Required(ErrorMessage = "First Name is a required field.")]
        [MinLength(2, ErrorMessage = "First Name requires a minimum of 2 Letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name may only contain the letters a-z and A-Z")]
        public string First_Name {get;set;}
        [Required(ErrorMessage = "Last Name is a required field.")]
        [MinLength(2, ErrorMessage = "Last Name requires a minimum of 2 Letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name may only contain the letters a-z and A-Z")]
        public string Last_Name {get;set;}
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
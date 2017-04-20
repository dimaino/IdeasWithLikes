using System;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegisterFinal
{
    public class PastDate : ValidationAttribute
    {
        private DateTime currentDate;
        public PastDate()
        {
            currentDate = DateTime.Now;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime setVal = (DateTime)value;
                if (setVal < currentDate)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult ("Wedding Date must be in the future.");
        }
    }
}
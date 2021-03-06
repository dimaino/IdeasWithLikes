using System;
using System.ComponentModel.DataAnnotations;

namespace BlackBeltTest2
{
    public class FutureDate : ValidationAttribute
    {
        private DateTime currentDate;
        public FutureDate()
        {
            currentDate = DateTime.Now;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime setVal = (DateTime)value;
                if (setVal > currentDate)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult ("Wedding Date must be in the future.");
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Section7.CustomValidationAttribute
{
    public class AgeLimit : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int age = 0;
            if (value != null && value is DateTime) {
                age = DateTime.Now.Year - ((DateTime)value).Year;
                if (age < 18)
                {
                    return new ValidationResult($"Current Age : {age}. Age Limit is 18");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Invalid value or Data Type");
        }
    }
}

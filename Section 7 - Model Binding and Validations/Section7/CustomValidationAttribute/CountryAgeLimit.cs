using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Section7.CustomValidationAttribute
{
    public class CountryAgeLimit : ValidationAttribute
    {
        private string _otherProperty { get; set; }

        //Assign Other Field property during construction initialization - [CountryAgeLimit("Country")]
        //Eg: [Compare("password")]
        public CountryAgeLimit(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int age = 0;
            if (value != null && value is DateTime)
            {
                age = DateTime.Now.Year - ((DateTime)value).Year;
                if (age < 18)
                {
                    return new ValidationResult($"Current Age : {age}. Age Limit is 18");
                }
                else
                {
                    //Get the datatype of the property using Reflection
                    PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);

                    //Get the value from ObjectInstance -> its the object with model data
                    object? otherPropertyValue = propertyInfo?.GetValue(validationContext.ObjectInstance);

                    if (otherPropertyValue != null)
                    {
                        string country = (string)otherPropertyValue;
                        if (country.Equals("UK"))
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult("Country Age limit did not match - Only UK base");
                        }
                    }
                    return new ValidationResult("Country - Invalid value or Data Type");
                }
            }
            return new ValidationResult("Age - Invalid value or Data Type");
        }
    }
}
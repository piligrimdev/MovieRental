using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AtLeast18IfAMember : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Customer customer = (Customer) validationContext.ObjectInstance;

            if(customer.MemberShipTypeId == 0 || customer.MemberShipTypeId == 1)
                return ValidationResult.Success;

            if(customer.BirthDay == null)
                return new ValidationResult("Date of birth is required");

            return DateTime.Now.Year - customer.BirthDay.Year >= 18
                ? ValidationResult.Success
                : new ValidationResult("Member should be at least 18 year old to be a member");

        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class NotContainCussing : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;
            if (movie.Description == null)
                return ValidationResult.Success;

            string[] words = movie.Description.Split();
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
            }

            return !words.Contains("блять")
                ? ValidationResult.Success
                : new ValidationResult("не ругайся козел");

        }
    }
}

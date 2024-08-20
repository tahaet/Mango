
using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;

        public MaxFileSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > (maxSize * 1024 * 1024))
                {
                    return new ValidationResult($"Maximum allowed file size is {maxSize} MB.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

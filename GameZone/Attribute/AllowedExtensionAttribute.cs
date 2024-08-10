using System.ComponentModel.DataAnnotations;

namespace GameZone.Attribute
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string _allowExtension;

        public AllowedExtensionAttribute(string allowExtension)
        {
            _allowExtension = allowExtension;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null )
            {
                var extension = Path.GetExtension(file.FileName);

                var isAllowed = _allowExtension.Split(',' ).Contains(extension ,StringComparer.OrdinalIgnoreCase);

                if( !isAllowed )
                    return new ValidationResult($"Only {_allowExtension} are Allowed! ");

            }
            return ValidationResult.Success;
        }
    }
}

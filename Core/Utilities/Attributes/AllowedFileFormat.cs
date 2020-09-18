﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Core.Utilities.Attributes
{
    public class AllowedFileFormat
    {
    }
    public class AllowedFileFormatAttribute : ValidationAttribute
    {
        private readonly string[] _formats;

        public AllowedFileFormatAttribute(params string[] formats)
        {
            _formats = formats;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_formats.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return "This photo extension is not allowed!";
        }
    }
}

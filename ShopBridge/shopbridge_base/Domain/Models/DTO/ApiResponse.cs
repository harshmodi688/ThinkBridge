using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Domain.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Validation = new Validation
            {
                Messages = new List<string>(),
                ValidationStatus = ValidationStatus.Success
            };
        }
        public T Data { get; set; }
        public Validation Validation { get; set; }
    }

    public class Validation
    {
        public List<string> Messages { get; set; }
        public ValidationStatus ValidationStatus { get; set; }
    }

    public enum ValidationStatus
    {
        Error,
        Success
    }
}

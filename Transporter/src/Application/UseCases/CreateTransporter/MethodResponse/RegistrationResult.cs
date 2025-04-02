using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.CreateTransporter.MethodResponse
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
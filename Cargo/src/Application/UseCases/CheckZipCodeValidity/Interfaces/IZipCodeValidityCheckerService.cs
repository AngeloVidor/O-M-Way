using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.CheckZipCodeValidity.Interfaces
{
    public interface IZipCodeValidityCheckerService
    {
        Task<bool> IsValidZipCodeAsync(string zipCode);
        Task<bool> IsValidZipCodeFormat(string zipCode);
    }
}
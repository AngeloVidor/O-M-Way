using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.ValidateCPF.Interface
{
    public interface IValidateCpfService
    {
        bool IsValid(string cpf);
        bool IsValidFormat(string cpf);
    }
}
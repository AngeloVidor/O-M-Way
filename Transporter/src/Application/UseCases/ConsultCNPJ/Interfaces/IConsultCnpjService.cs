using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.ConsultCNPJ.Interfaces
{
    public interface IConsultCnpjService
    {
        Task<bool> IsCnpjValidAsync(string cnpj);
    }
}
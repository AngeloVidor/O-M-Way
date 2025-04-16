using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.UseCases.ValidateCPF.Interface;

namespace src.Application.UseCases.ValidateCPF.Implementation
{
    public class ValidateCpfService : IValidateCpfService
    {
        public bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) throw new ArgumentNullException(nameof(cpf), "CPF cannot be null or empty.");
            var isValidFormat = IsValidFormat(cpf);
            if (!isValidFormat) throw new ArgumentException("Invalid CPF format.");

            if (cpf.Length != 11) return false;
            if (cpf.Distinct().Count() == 1) return false;

            var baseNumber = cpf.Substring(0, 9).Select(c => int.Parse(c.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += baseNumber[i] * (10 - i);
            }

            int firstDigit = sum % 11;
            firstDigit = firstDigit < 2 ? 0 : 11 - firstDigit;

            sum = 0;
            var baseWithFirst = baseNumber.Append(firstDigit).ToArray();
            for (int i = 0; i < 10; i++)
            {
                sum += baseWithFirst[i] * (11 - i);
            }

            int secondDigit = sum % 11;
            secondDigit = secondDigit < 2 ? 0 : 11 - secondDigit;

            int originalFirst = int.Parse(cpf[9].ToString());
            int originalSecond = int.Parse(cpf[10].ToString());
            return firstDigit == originalFirst && secondDigit == originalSecond;
        }

        public bool IsValidFormat(string cpf)
        {
            for (int i = 0; i < cpf.Length; i++)
            {
                if (!char.IsDigit(cpf[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
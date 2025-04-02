using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using src.Application.UseCases.ConsultCNPJ.Interfaces;
using src.Application.UseCases.ConsultCNPJ.Response;

namespace src.Application.UseCases.ConsultCNPJ.Imlementations
{
    public class ConsultCnpjService : IConsultCnpjService
    {
        private readonly HttpClient _httpClient;

        public ConsultCnpjService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsCnpjValidAsync(string cnpj)
        {
            var normalizedCnpj = NormalizeCnpj(cnpj);
            if (normalizedCnpj == null) return false;

            var requestUrl = $"https://receitaws.com.br/v1/cnpj/{cnpj}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                    return false;

                string responseBody = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseBody))
                    return false;

                var responseContent = JsonSerializer.Deserialize<ResponseContent>(responseBody);
                return responseContent?.situacao == "ATIVA";
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public string? NormalizeCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return null;

            string validCnpjFormat = Regex.Replace(cnpj, @"\D", "");

            return validCnpjFormat.Length == 14 ? validCnpjFormat : null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
using src.Application.UseCases.CheckZipCodeValidity.Response;

namespace src.Application.UseCases.CheckZipCodeValidity.Implementations
{
    public class ZipCodeValidityCheckerService : IZipCodeValidityCheckerService
    {
        private readonly HttpClient _httpClient;

        public ZipCodeValidityCheckerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsValidZipCodeAsync(string zipCode)
        {
            bool isZipCodeValid = await IsValidZipCodeFormat(zipCode);
            if (!isZipCodeValid)
                return false;

            string requestUrl = $"https://viacep.com.br/ws/{zipCode}/json/";
            
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                    return false;

                string responseBody = await response.Content.ReadAsStringAsync();
                var address = JsonSerializer.Deserialize<Address>(responseBody);

                return address != null && !string.IsNullOrEmpty(address.cep);
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> IsValidZipCodeFormat(string zipCode)
        {
            if (zipCode.Length != 8)
            {
                return false;
            }
            for (int i = 0; i < zipCode.Length; i++)
            {
                if (!char.IsDigit(zipCode[i]))
                    return false;
            }
            return true;
        }
    }
}


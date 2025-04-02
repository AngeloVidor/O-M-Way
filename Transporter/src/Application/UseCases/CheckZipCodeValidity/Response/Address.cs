using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.CheckZipCodeValidity.Response
{
    public class Address
    {
        public string cep { get; set; }
        public string localidade { get; set; }
        public string estado { get; set; }
        public string regiao { get; set; }
    }
}
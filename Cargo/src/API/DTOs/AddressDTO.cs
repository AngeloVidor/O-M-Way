using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.API.DTOs
{
    public class AddressDTO
    {
        public string cep { get; set; }
        public string localidade { get; set; }
        public string estado { get; set; }
        public string regiao { get; set; }
    }
}
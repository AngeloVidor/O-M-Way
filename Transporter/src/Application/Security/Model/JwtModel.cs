using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.Security.Model
{
    public class JwtModel
    {
        public string JWT_KEY { get; set; }
        public string JWT_ISSUER { get; set; }
        public string JWT_AUDIENCE { get; set; }
        public int JWT_DurationInMinutes { get; set; }

    }
}
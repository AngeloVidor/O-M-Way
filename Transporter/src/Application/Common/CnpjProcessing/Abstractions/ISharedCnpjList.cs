using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructure.Broker.Events.CnpjRequested.Models;

namespace src.Application.Common.CnpjProcessing.Abstractions
{
    public interface ISharedCnpjList
    {
        Task<ConsultRequest> HandleAsync(ConsultRequest request);
    }
}
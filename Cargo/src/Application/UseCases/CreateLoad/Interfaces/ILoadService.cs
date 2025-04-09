using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;
using src.Application.Common;

namespace src.Application.UseCases.CreateLoad.Interfaces
{
    public interface ILoadService
    {
        Task<MethodResponse> CreateLoadAsync(LoadDTO load);

    }
}
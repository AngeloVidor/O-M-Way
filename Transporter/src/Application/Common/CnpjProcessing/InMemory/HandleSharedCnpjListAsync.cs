using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using src.Application.Common.CnpjProcessing.Abstractions;
using src.Infrastructure.Broker.Events.CnpjRequested.Models;

namespace src.Application.Common.CnpjProcessing.InMemory
{
    public class HandleSharedCnpjListAsync : ISharedCnpjList
    {
        private readonly ConcurrentQueue<ConsultRequest> _requests = new ConcurrentQueue<ConsultRequest>();
        private readonly int _rateLimit = 3;
        private readonly ILogger<HandleSharedCnpjListAsync> _logger;

        public HandleSharedCnpjListAsync(ILogger<HandleSharedCnpjListAsync> logger)
        {
            _logger = logger;
        }

        public async Task<ConsultRequest> HandleAsync(ConsultRequest request)
        {
            _logger.LogInformation("Iniciando processamento para CNPJ: {CNPJ}", request.CNPJ);
            DateTime now = DateTime.Now;

            while (_requests.TryPeek(out var oldest) && (now - oldest.TimeAddedToList).TotalSeconds > 60)
            {
                if (_requests.TryDequeue(out var expiredItem))
                {
                    _logger.LogDebug("Item expirado removido da fila: {CNPJ} (Tempo na fila: {Tempo}s)",
                        expiredItem.CNPJ, (now - expiredItem.TimeAddedToList).TotalSeconds);
                }
            }

            if (_requests.Count < _rateLimit)
            {
                request.TimeAddedToList = now;
                _requests.Enqueue(request);
                _logger.LogInformation("Novo item adicionado à fila. CNPJ: {CNPJ} | Itens na fila: {Count}",
                    request.CNPJ, _requests.Count);
                return request;
            }

            if (_requests.TryPeek(out var nextToExpire))
            {
                TimeSpan timeSinceOldest = now - nextToExpire.TimeAddedToList;
                int timeToWait = 60_000 - (int)timeSinceOldest.TotalMilliseconds;

                if (timeToWait > 0)
                {
                    _logger.LogWarning("Limite de taxa atingido. Aguardando {Tempo}ms para CNPJ: {CNPJ}", timeToWait, request.CNPJ);
                    await Task.Delay(timeToWait);
                }

                if (_requests.TryDequeue(out var dequeuedItem))
                {
                    _logger.LogDebug("Item removido após espera: {CNPJ}", dequeuedItem.CNPJ);
                    request.TimeAddedToList = DateTime.Now;
                    _requests.Enqueue(request);
                    _logger.LogInformation("Novo item reinserido após espera. CNPJ: {CNPJ}", request.CNPJ);
                    return request;
                }

                _logger.LogError("Falha ao remover item da fila após espera para CNPJ: {CNPJ}", request.CNPJ);
                throw new InvalidOperationException("Failed to remove item from the queue after waiting");
            }

            request.TimeAddedToList = DateTime.Now;
            _requests.Enqueue(request);
            _logger.LogInformation("Item adicionado em condição especial. CNPJ: {CNPJ}", request.CNPJ);
            return request;
        }
    }
}
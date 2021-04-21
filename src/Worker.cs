using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace barbeiro_dorminhoco
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBarbeiro _barbeiro;

        public Worker(ILogger<Worker> logger, IBarbeiro barbeiro)
        {
            _logger = logger;
            _barbeiro = barbeiro;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _barbeiro.StartWorking();
            while (!stoppingToken.IsCancellationRequested)
            {
                var cliente = new Cliente
                {
                    Name = Guid.NewGuid().ToString()
                };
                cliente.EntrarNaFila();
                _logger.LogInformation($"Client {cliente.Name} arrived at: {DateTimeOffset.Now} and Barber sleeped {_barbeiro.TempoDormido}");
                await Task.Delay(3000, stoppingToken);
            }
            _barbeiro.StopWorking();
        }
    }
}

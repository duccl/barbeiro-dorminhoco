using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace barbeiro_dorminhoco
{
    public class Barbeiro: IBarbeiro
    {
        #region .: Properties :.
            
        private readonly Thread Worker;
        private readonly ILogger<Barbeiro> _logger;
        public string Id { get; }
        private const int WorkTime = 20000;

        #endregion

        #region .: Constructor :.

        public Barbeiro(ILogger<Barbeiro> logger)
        {
            Id = System.Guid.NewGuid().ToString();
            Worker = new Thread(new ThreadStart(this.DoWork));
            _logger = logger;
        }

        #endregion

        #region .: Methods :.

        public void StartWorking() => Worker.Start();
        public void StopWorking() => Worker.Interrupt();

        public void DoWork()
        {
            int startTime = 0;
            while (true)
            {
                if(startTime > WorkTime) break;

                if(Clientes.Fila.Count > 0) 
                    AtenderCliente(Clientes.Fila.Dequeue());

                startTime++;
            }
        }

        private void AtenderCliente(Cliente cliente)
        {
            _logger.LogInformation($"Opa, cliente {cliente.Name} sendo atendido! Ele estava {cliente.TempoAguardando} aguardando tudo isso");
            _logger.LogInformation($"Vou demorar {cliente.TempoParaCortarCabelo} para cortar o cabelo..");
            Thread.Sleep(cliente.TempoParaCortarCabelo);
            _logger.LogInformation($"Ufa consegui cortar!");
        }

        #endregion
    }
}
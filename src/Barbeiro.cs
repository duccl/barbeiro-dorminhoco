using System.Threading;
using Microsoft.Extensions.Logging;

namespace barbeiro_dorminhoco
{
    public class Barbeiro : IBarbeiro
    {
        #region .: Properties :.

        private readonly Thread Worker;
        private readonly ILogger<Barbeiro> _logger;
        public string Id { get; }
        public const int TempoMaximoEntreClientes = 3000;
        public int TempoDormido { get; set; } = 0;

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
            while (true)
            {
                if (Clientes.Fila.Count > 0)
                {
                    var cliente = Clientes.Fila.Dequeue();
                    AtenderCliente(cliente);
                    TentarDormir(Clientes.Fila.Count, cliente.TempoParaCortarCabelo);
                }

            }
        }

        private void TentarDormir(int clientesNaFila, int tempoDeCorteDoClienteAnterior)
        {
            int tempoParaDormir = TempoMaximoEntreClientes - tempoDeCorteDoClienteAnterior;
            _logger.LogInformation($"Sobraram {tempoParaDormir} para eu dormir! Vamos ver se consigo");
            if (clientesNaFila == 0)
            {
                _logger.LogWarning($"Vou dormir um pouco...");
                Thread.Sleep(tempoParaDormir);
                TempoDormido += tempoParaDormir;
                _logger.LogWarning("Poxa vamos trabalhar ne..");
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
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
        public const int TempoMaximoEntreClientes = 3000;
        public int TempoDormido {get; set;} = 0;

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
                if(Clientes.Fila.Count > 0) 
                    AtenderCliente(Clientes.Fila.Dequeue());
            }
        }

        private void AtenderCliente(Cliente cliente)
        {
            _logger.LogInformation($"Opa, cliente {cliente.Name} sendo atendido! Ele estava {cliente.TempoAguardando} aguardando tudo isso");
            _logger.LogInformation($"Vou demorar {cliente.TempoParaCortarCabelo} para cortar o cabelo..");
            Thread.Sleep(cliente.TempoParaCortarCabelo);
            int tempoParaDormir = TempoMaximoEntreClientes-cliente.TempoParaCortarCabelo;
            _logger.LogInformation($"Ufa consegui cortar!");
            _logger.LogInformation($"Sobraram {tempoParaDormir} para eu dormir!");
            _logger.LogInformation($"Vou dormir um pouco...");
            Thread.Sleep(tempoParaDormir);
            TempoDormido+= tempoParaDormir;
            _logger.LogInformation("Poxa vamos trabalhar ne..");
        }

        #endregion
    }
}
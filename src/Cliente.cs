
using System;
using System.Threading;

namespace barbeiro_dorminhoco
{
    public class Cliente
    {
        #region .: Properties :.

        private Thread Worker;
        public string Name { get; set; }
        public TimeSpan TempoAguardando { get; set; } = default;
        public int TempoParaCortarCabelo => new Random().Next(0, 4000);

        public bool Atendido { get; set; }

        #endregion

        #region .: Constructor :.

        public Cliente()
        {
            Worker = new Thread(new ThreadStart(Wait));
        }

        #endregion

        #region .: Methods :.

        public void EntrarNaFila()
        {
            Clientes.Fila.Enqueue(this);
            Worker.Start();
        }

        public void Wait()
        {
            var horaEntrada = DateTime.Now;
            while (!Atendido)
            {
                TempoAguardando = DateTime.Now - horaEntrada;
                Thread.Sleep(10);
            }
        }

        #endregion
    }
}
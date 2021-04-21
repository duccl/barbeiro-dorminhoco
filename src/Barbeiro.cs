using System;
using System.Threading;

namespace barbeiro_dorminhoco
{
    public class Barbeiro
    {
        private readonly Thread Worker;
        public string Id { get; }
        public Barbeiro()
        {
            Id = System.Guid.NewGuid().ToString();
            Worker = new Thread(new ThreadStart(this.DoWork));
        }

        public void StartWorking() => Worker.Start();
        public void StopWorking() => Worker.Interrupt();

        public void DoWork()
        {
            Console.WriteLine("I am Working!");
        }
    }
}
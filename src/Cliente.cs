
using System;

namespace barbeiro_dorminhoco
{
    public class Cliente
    {
        public string Name { get; set; }
        public double TempoAguardando { get; set; }
        public int TempoParaCortarCabelo => new Random(3).Next(0,4000);
    }
}
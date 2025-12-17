using System;
using System.Collections.Generic;
using System.Text;

namespace BDOO
{
    internal class Moto : Vehiculo
    {
        public int Cilindrada;

        public override string ToString()
        {
            return $"[MOTO] {base.ToString()} ({Cilindrada} CC)";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BDOO
{
    internal class Auto : Vehiculo
    {

        public int Puertas;

        public override string ToString()
        {
            return $"[AUTO] {base.ToString()} ({Puertas} Puertas)";
        }
    }
}

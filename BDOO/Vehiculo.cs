using System;
using System.Collections.Generic;
using System.Text;

namespace BDOO
{
    internal class Vehiculo
    {
        public string Marca;
        public string Modelo;
        public double Precio;

        public override string ToString()
        {
            return $"{Marca} {Modelo} - ${Precio}";
        }
    }
}

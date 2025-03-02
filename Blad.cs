using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    internal class Blad : Exception
    {
        public Blad(string? message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}

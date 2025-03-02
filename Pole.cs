using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    public class Pole
    {
        public int x;
        public int y;
        public int Stan;
        public Pole(int i, int j)
        {
            x = i; y = j;
            Stan = (int)Colors.Pusty;
        }

        public int ShowColor { get { return Stan; } }

        public void zmienStan(int kolor)
        {
            this.Stan = kolor;
        }

        
    }
}

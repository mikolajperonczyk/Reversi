using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    internal class AI(int color) : Gracz(color)
    {
        public override Pole Ruch(List<Pole> dostepne)
        {
            Random random = new Random();

            return dostepne[random.Next(dostepne.Count)];
        }
    }
    
}

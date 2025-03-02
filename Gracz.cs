using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    

    internal abstract class Gracz
    {
        public int kolor;
        int punkty;
        
        public Gracz(int color) 
        {
            kolor = color;
            punkty = 2;
        }

        public int Points { get { return punkty; } }
        public int Color { get { return kolor; } }
        public int Update { set { punkty = value; } }

        public virtual Pole TCS {
            get;
            set; 
        }

        public void Reset()
        {
            punkty = 0;
        }

        public void CountPoints(Plansza plansza)
        {
            int counter = 0;
            foreach (Pole pole in plansza.Tab)
                if (pole.ShowColor == kolor) counter++;

            punkty = counter;
        }

        public virtual Pole Ruch(List<Pole> dostepne)
        {
            throw new NotImplementedException();
        }
    }
}

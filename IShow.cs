using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    internal interface IShow
    {
        public void Wyswietl(Plansza plansza, Graphics e);
        
        public void Wyczysc(Plansza plansza, Graphics e);

        public void WyswietlKolor(Graphics e, int size, Gracz Aktywny);

        public void WyczyscKolor(Graphics e);


    }
}

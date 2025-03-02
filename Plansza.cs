using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Projekt_PO2_Reversi
{

    public class Plansza
    {

        public int HW;
        public int Dimension;
        public Pole[,] pola;

        public int Dim { get { return Dimension; } }
        public Pole this[int i, int j] { get { return pola[i, j]; } }  
        public Pole[,] Tab { get { return pola; } }
        public Plansza(int HW, int Dimension)
        {
           
            this.HW = HW;
            this.Dimension = Dimension;

            pola = new Pole[Dimension,Dimension];
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    pola[i,j] = new Pole(i,j);
        }

        public void ClearAvl()
        {
            foreach (Pole i in pola)
            {
                if (i.ShowColor == (int)Colors.Dostepny)
                    i.zmienStan((int)Colors.Pusty);
            }
        }

        public int Showfield(int x, int y)
        {
            return pola[x,y].ShowColor;
        }

        public void SetChange(int x, int y, int color)
        {
            pola[x, y].zmienStan(color);
        }

        public bool FullBoard()
        {
            foreach(Pole i in pola)
                if (i.ShowColor == (int)Colors.Pusty || i.ShowColor == (int)Colors.Dostepny) return false;
            return true;
        }

    }
}

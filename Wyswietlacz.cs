using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    internal class Wyswietlacz : IShow
    {
        public Wyswietlacz() { }
        public void Wyswietl(Plansza plansza, Graphics e)
        {
            Pen pen = new Pen(Color.Green);
            Pen Whites = new Pen(Color.Black);
            Pen Blacks = new Pen(Color.White);

            float FieldSize = (float)plansza.HW / plansza.Dim;
            float move = (float)((FieldSize - (0.8 * FieldSize)) / 2);
            float AvlMove = (float)((FieldSize -  (0.3 * FieldSize)) / 2);

            pen.Brush = new SolidBrush(Color.FromArgb(0, 168, 107));
            Whites.Brush = new SolidBrush(Color.White);
            Blacks.Brush = new SolidBrush(Color.Black);

            for (int i = 0; i < plansza.Dim; i++)
                for (int j = 0; j < plansza.Dim; j++)
                {
                    e.FillRectangle(pen.Brush, i * FieldSize + 1, j * FieldSize + 1, FieldSize - 2, FieldSize - 2);
                    if (plansza.Showfield(i,j) == (int)Colors.Bialy)
                        e.FillEllipse(Whites.Brush, i * FieldSize + move, j * FieldSize + move, (int)(FieldSize * 0.8), (int)(FieldSize * 0.8));
                    else if (plansza.Showfield(i,j) == (int)Colors.Czarny) e.FillEllipse(Blacks.Brush, i * FieldSize + move, j * FieldSize + move, (int)(FieldSize * 0.8), (int)(FieldSize * 0.8));
                    else if (plansza.Showfield(i, j) == (int)Colors.Dostepny) 
                        e.FillRectangle(Blacks.Brush, (i * FieldSize) + AvlMove, (j * FieldSize) + AvlMove, (int)(FieldSize * 0.3), (int)(FieldSize * 0.3));
                    
                }
        }

        public void Wyczysc(Plansza plansza, Graphics e)
        {
            e.Clear(Color.Black);
        }

        public void WyswietlKolor(Graphics e, int size, Gracz Aktywny) 
        {
            Pen pen = new Pen(Color.Black);
            pen.Brush = new SolidBrush(Color.Black);

            if (Aktywny.Color == (int)Colors.Bialy)
                e.DrawRectangle(pen, 1, 1, size - 1, size - 1);
            else
                e.FillRectangle(pen.Brush, 1, 1, size - 1, size - 1);

        }

        public void WyczyscKolor(Graphics e)
        {
            e.Clear(Color.White);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO2_Reversi
{
    public class Logika
    {
        public void BringToStart(Plansza plansza)
        {
            int middle = plansza.Dim / 2;
            for (int i = 0; i < plansza.Dim; i++)
                for (int j = 0; j < plansza.Dim; j++)
                    plansza.SetChange(i, j, (int)Colors.Pusty);
            plansza.SetChange(middle, middle, (int)Colors.Bialy);
            plansza.SetChange(middle - 1, middle - 1, (int)Colors.Bialy);
            plansza.SetChange(middle - 1, middle, (int)Colors.Czarny);
            plansza.SetChange(middle, middle - 1, (int)Colors.Czarny);

        }
        // Główna metoda do znajdowania dostępnych ruchów wraz ze ścieżkami do nich
        public List<List<Pole>> Checkmoves(Plansza plansza, int Color) // kolor rozgrywającego
        {
            List<List<Pole>> moves = new List<List<Pole>>();
            List<int> neighbs = new List<int>();
            List<Pole>? OneWay = new List<Pole>();

            int[] newMoves = new int[2];

            for (int i = 0; i < plansza.Dim; i++) // przeszukania całej planszy
                for (int j = 0; j < plansza.Dim; j++)
                    if (plansza.Showfield(i, j) == Color) // warunek czy natrafione pole ma kolor gracza
                    {
                        neighbs = CheckNeighbors(plansza, i, j, Color); // przeszukanie dookoła pola w celu znalezienia pionka przeciwnika
                        foreach (int n in neighbs)
                        {
                            OneWay = CheckWay(plansza, i, j, Color, n); // sprawdzenie każdej ze w stronę pionków przeciwnika do wyjscia poza plansze lub natrafienia na puste pole
                            if (OneWay != null)
                                moves.Add(OneWay);
                        }
                    }

            return moves;
        }

        // Metoda testująca czy nie wychodzi poza tablicę pomocnicza dla metody Check Neighbors
        private static bool IsValid(int row, int col, int rows, int cols)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }

        // Metoda zwracająca jedną ścieżkę
        // Podrzędna CheckNeighbours
        public List<Pole>? CheckWay(Plansza plansza, int x, int y, int color, int way)
        {
            List<Pole> moves = new List<Pole>();
            int[] Coords = new int[2];
            int[] NewWay = DirToInts(way);
            int enemy = OtherColor(color);

            Coords[0] = x; Coords[1] = y;

            for (int i = 0; i < NewWay.Length; i++)
                Coords[i] += NewWay[i];

            while (IsValid(Coords[0], Coords[1], plansza.Dim, plansza.Dim))
            {

                if (plansza.Showfield(Coords[0], Coords[1]) == (int)Colors.Pusty || plansza.Showfield(Coords[0], Coords[1]) == (int)Colors.Dostepny )
                {
                    moves.Add(plansza[Coords[0], Coords[1]]);
                    return moves;
                }
                
                if (plansza.Showfield(Coords[0], Coords[1]) == enemy)
                {
                    moves.Add(plansza[Coords[0], Coords[1]]);
                }

                if (plansza.Showfield(Coords[0], Coords[1]) == color)
                    return null;

                for (int i = 0; i < NewWay.Length; i++)
                    Coords[i] += NewWay[i];
            }

            return null;
        }

        // Metoda zbierająca dostępne ścieżki z wybranego punktu
        // Metoda podrzędna metody Checkmoves
        public List<int> CheckNeighbors(Plansza plansza, int row, int col, int Color)
        {
            int enemyColor = OtherColor(Color);

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            List<int> results = new List<int>();


            int HW = plansza.Dim;

            for (int i = 0; i < dx.Length; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];

                if (IsValid(newRow, newCol, HW, HW))
                {
                    //Console.WriteLine($"Neighbor at ({newRow}, {newCol}): {array[newRow, newCol]}");
                    //CheckLine(newRow,newCol,);
                    if (plansza.Showfield(newRow, newCol) == enemyColor)
                    {
                        results.Add(IntsTohDir(dx[i], dy[i]));
                    }
                }

            }
            return results;
        }

        // Wyłuskanie dostępnych pól do wykonania ruchu
        public List<Pole> ToAvl(List<List<Pole>> poles) 
        { 
            List<Pole> res = new List<Pole>();
            foreach (List<Pole> p in poles)
            {
                res.Add(p.Last());
            }
            return res;
        }

        // Zmiana koloru pionków na ścieżce po wykonaniu ruchu
        public void ChangeColorsOfPaths(List<Pole> Path, int ToColor)
        {
            foreach (Pole p in Path)
            {
                p.zmienStan(ToColor);
            }
        }

        // Metoda wybierająca kolor przeciwny (z białego na czarny i z czarnego na biały)
        public int OtherColor(int Color)
        {
            if (Color == (int)Colors.Bialy) return (int)Colors.Czarny;
            if (Color == (int)Colors.Czarny) return (int)Colors.Bialy;
            return (int)Colors.Pusty;
        }

        // Metoda tłumacząca wektory długości 1 na kierunki świata (upraszczająca nieco kod)
        public static int IntsTohDir(int x, int y)
        {
            if (x == 0 && y == 1) return (int)Compas.S;
            if (x == 1 && y == 1) return (int)(Compas.SE);
            if (x == 1 && y == 0) return (int)(Compas.E);
            if (x == 1 && y == -1) return (int)(Compas.NE);
            if (x == 0 && y == -1) return (int)Compas.N;
            if (x == -1 && y == -1) return (int)Compas.NW;
            if (x == -1 && y == 0) return (int)Compas.W;
            if (x == -1 && y == 1) return (int)(Compas.SW);
            throw new Blad("BLADJAKIS");
        }
        public static int[] DirToInts(int compas)
        {
            int[] ints = new int[2];
            switch (compas)
            {
                case (int)Compas.S:
                    ints[0] = 0;
                    ints[1] = 1;
                    break;
                case (int)Compas.SE:
                    ints[0] = 1;
                    ints[1] = 1;
                    break;
                case (int)Compas.NE:
                    ints[0] = 1;
                    ints[1] = -1;
                    break;
                case (int)Compas.N:
                    ints[0] = 0;
                    ints[1] = -1;
                    break;
                case (int)Compas.NW:
                    ints[0] = -1;
                    ints[1] = -1;
                    break;
                case (int)Compas.W:
                    ints[0] = -1;
                    ints[1] = 0;
                    break;
                case (int)Compas.SW:
                    ints[0] = -1;
                    ints[1] = 1;
                    break;
                case (int)Compas.E:
                    ints[0] = 1;
                    ints[1] = 0;
                    break;
            }
            return ints;
        }

        // Warunki kończące rozgrywkę i prowadzące do podliczenia punktów
        public bool EndingCons(Plansza plansza, List<Pole> dst)
        {
            if (plansza.FullBoard()) return true;
            if (dst.Count == 0) return true; // do zmiany raczej

            return false;   
        }

        // Znajduje ścieżkę po wykonanym ruchu
        public List<List<Pole>>? FindThePathsOfAvl(Plansza plansza, List<List<Pole>> poles, Pole dostepne)
        {
            List<List<Pole>> way = new List<List<Pole>>();

            foreach (List<Pole> path in poles)
            {
                if (path.Last().Equals(dostepne)) way.Add(path);
            }
            
            return way;
        }

        
    }
}

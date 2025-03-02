using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projekt_PO2_Reversi
{
    enum Compas
    {
        N = 0,
        NE = 1,
        E = 2,
        SE = 3,
        S = 4,
        SW = 5,
        W = 6,
        NW = 7,
    }
    
    internal class Gra
    {
        Plansza plansza;
        IShow Wyswietlacz;
        List<Gracz> gracze;
        public Gracz AktywnyGracz;
        
        internal Logika Logic;
        internal PictureBox pB;
        internal PictureBox pBKolor;
        internal Serializer jsonSerializer;



        public Gra(int HW, int dim, bool Komp, PictureBox pictureBox, PictureBox pictureBox2) 
        { 
            plansza = new Plansza(HW, dim);
            pB = pictureBox;
            pBKolor = pictureBox2;
            Logic = new Logika();
            jsonSerializer = new Serializer(); 

            Wyswietlacz = new Wyswietlacz();
            
            gracze = new List<Gracz>();
            gracze.Add(new Human((int)Colors.Bialy));
            if (Komp)
                gracze.Add(new AI((int)Colors.Czarny));
            else
                gracze.Add(new Human((int)Colors.Czarny));
            
        }
        
        public List<Gracz> PLAYERS { get { return gracze; } }
        public int HW { get { return plansza.HW;  } set { plansza.HW = value; } }

        public Pole ClickToField(MouseEventArgs args)
        {
            float FieldSize = (float)HW / plansza.Dim;
            int whichX = (int)(args.X / FieldSize);
            int whichY = (int)(args.Y / FieldSize);

            return plansza[whichX, whichY];

        }

        

        

        public void Clear(Graphics e) => Wyswietlacz.Wyczysc(plansza, e);

        public void Draw(Graphics e) => Wyswietlacz.Wyswietl(plansza, e);

        public void NewGameplay()
        {
            bool first = true;
            Gracz wygrany;
            while (true)
                foreach (Gracz gracz in gracze)
                    if (AktywnyGracz.Color != gracz.Color && first)
                    {
                        first = false;
                        continue;
                    }
                    else
                        if (!Round(gracz))
                    {
                        wygrany = End();
                        MessageBox.Show("Zwycięża gracz " + Int2String(wygrany.Color) + " z ilością punktów = " + wygrany.Points);
                        return;
                    }
                    
        }
        public void Gameplay()
        {
            Gracz Wygrany;
            Logic.BringToStart(plansza);
            while (true)
                foreach (Gracz gracz in gracze)
                    if (!Round(gracz))
                    {
                        Wygrany = End();
                        MessageBox.Show("Zwycięża gracz " + Int2String(Wygrany.Color) + " z ilością punktów = " + Wygrany.Points);
                        return;
                    }
                             
            
        }
        internal bool Round(Gracz gracz)
        {
            List<List<Pole>> list = new List<List<Pole>>();
            List<Pole> dostepne = new List<Pole>();
            List<List<Pole>> sciezki = new List<List<Pole>>();
            Pole Pionek;

            AktywnyGracz = gracz;


            list = Logic.Checkmoves(plansza, gracz.Color);

            dostepne = Logic.ToAvl(list);

            foreach (Pole dst in dostepne)
                dst.zmienStan((int)Colors.Dostepny);



            if (Logic.EndingCons(plansza, dostepne))
            {
                Console.WriteLine("KONIEC");
                return false;
            }

            Pionek = AktywnyGracz.Ruch(dostepne);

            foreach (Pole p in dostepne)
            {
                sciezki = Logic.FindThePathsOfAvl(plansza, list, Pionek);
                foreach (List<Pole> lista in sciezki)
                    Logic.ChangeColorsOfPaths(lista, gracz.Color);

            }


            plansza.ClearAvl();
            pB.Invalidate();
            pBKolor.Invalidate();


            foreach (Gracz gracz1 in gracze)
                gracz1.CountPoints(plansza);

            return true;
        }

        string Int2String(int kolor)
        {
            if (kolor == (int)Colors.Czarny) return "Czarny";
            else return "Bialy";
        }

        public void Deserialize(string json)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            jsonSerializer = JsonConvert.DeserializeObject<Serializer>(json,jsonSerializerSettings);

            this.gracze = jsonSerializer.graczes;
            this.plansza = jsonSerializer.Plansza;
            this.AktywnyGracz = jsonSerializer.AktywnyGracz;

            foreach (Gracz gracz1 in gracze)
                gracz1.CountPoints(plansza);

            pB.Invalidate();
            pBKolor.Invalidate();
        }
        public string Serialize()
        {
            return jsonSerializer.SerializeIt(plansza, gracze, AktywnyGracz);
        }
        
        
        public void ToHuman(Pole pole)
        {
            if(AktywnyGracz.GetType() == typeof(Human))
                AktywnyGracz.TCS = pole;
        }

        public void DrawMove(Graphics e, int size)
        {
            Wyswietlacz.WyswietlKolor(e, size, AktywnyGracz);
        }

        public void ClearMove(Graphics e)
        {
            Wyswietlacz.WyczyscKolor(e);
        }

        public Gracz End()
        {
            int best = -1;
            Gracz Bgracz = new Human(-1);
            foreach (Gracz gracz in gracze)
            {
                if (gracz.Points > best)
                {
                    best = gracz.Points;
                    Bgracz = gracz; 
                }
            }
            return Bgracz;
        }

        

        
    }
}




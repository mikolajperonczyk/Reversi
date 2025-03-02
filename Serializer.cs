using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projekt_PO2_Reversi
{
    [Serializable]
    internal class Serializer
    {
        public Plansza Plansza;
        public List<Gracz> graczes;
        public Gracz AktywnyGracz;
        
        [NonSerialized]
        public JsonSerializerSettings jsonSerializerSettings;
        string jsonString;
        public Serializer()
        {
            
        }

        public string SerializeIt(Plansza plansza, List<Gracz> gracze, Gracz AktywnyGracz)
        {
            Plansza = plansza;
            this.graczes = gracze;
            this.AktywnyGracz = AktywnyGracz;
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (Plansza != null)
                jsonString = JsonConvert.SerializeObject(this, jsonSerializerSettings);

            return jsonString;
        }

        

    }
}

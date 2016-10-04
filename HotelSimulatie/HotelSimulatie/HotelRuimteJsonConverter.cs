using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using HotelSimulatie.Model;
using Newtonsoft.Json.Linq;

namespace HotelSimulatie
{
    public class HotelRuimteJsonConverter : JsonConverter
    {
        
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(HotelRuimte));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            if(jObject["AreaType"].Value<string>() == "Restaurant")
            {
                return jObject.ToObject<Eetzaal>(serializer);
            }
            else if (jObject["AreaType"].Value<string>() == "Cinema")
            {
                return jObject.ToObject<Bioscoop>(serializer);
            }
            else if (jObject["AreaType"].Value<string>() == "Fitness")
            {
                return jObject.ToObject<Fitness>(serializer);
            }
            else if (jObject["AreaType"].Value<string>() == "Room")
            {
                return jObject.ToObject<Kamer>(serializer);
            }
            else
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Writen is niet nodig en dus ook niet geimplementeerd.
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using HotelSimulatie.Model;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

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
            // Verandert stringcoordinaten naar vectors
            JObject jObject = JObject.Load(reader);
            Regex coordinatenRegex = new Regex(@"([1-9])([,]\s?)([1-9])");

            JObject testObject = new JObject();

            IEnumerable<JProperty> propertyLijst = jObject.Properties();
            /*
                if(jProperty.Value.Type == JTokenType.String && coordinatenRegex.IsMatch((string)jProperty.Value))
                {
                }*/


            // Zet de objecten om naar de juiste childrens van hotelruimte
            if (jObject["AreaType"].Value<string>() == "Restaurant")
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
                string classification = jObject["Classification"].Value<string>();
                char a = classification[0];
                jObject.Property("Classification").Value = (int)Char.GetNumericValue(a);
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
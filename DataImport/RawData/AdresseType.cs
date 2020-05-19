using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RICAssemblee.DataImport.RawData
{
    public enum AdresseType
    {
        Mail,
        Telephonique,
        Postale,
        SiteWeb,
    }

    internal class AdresseTypeConverter : JsonConverter
    {
        public static AdresseTypeConverter Singleton = new AdresseTypeConverter();

        public override bool CanConvert(Type t) => t == typeof(AdresseType) || t == typeof(AdresseType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value.ToLowerInvariant())
            {
                case "adressemail_type":
                    return AdresseType.Mail;
                case "adressetelephonique_type":
                    return AdresseType.Telephonique;
                case "adressepostale_type":
                    return AdresseType.Postale;
                case "adressesiteweb_type":
                    return AdresseType.SiteWeb;
                default:
                    throw new NotImplementedException(string.Format($"cannot convert adress type : {value}"));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

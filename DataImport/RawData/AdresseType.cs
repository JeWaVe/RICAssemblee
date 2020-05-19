using Newtonsoft.Json;
using System;

namespace RICAssemblee.DataImport.RawData
{
    public enum AdresseType
    {
        Officielle,
        Circonscription,
        Telephone,
        Fax,
        Mail,
        SiteWeb,
        Facebook,
        Twitter,
        UrlSenateur
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
                case "0":
                    return AdresseType.Officielle;
                case "2":
                    return AdresseType.Circonscription;
                case "11":
                    return AdresseType.Telephone;
                case "12":
                    return AdresseType.Fax;
                case "15":
                    return AdresseType.Mail;
                case "22":
                    return AdresseType.SiteWeb;
                case "25":
                    return AdresseType.Facebook;
                case "24":
                    return AdresseType.Twitter;
                case "23":
                    return AdresseType.UrlSenateur;
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

using Newtonsoft.Json;
using System;

namespace RICAssemblee.DataImport.RawData
{
    public enum MandatType { MandatMission, MandatParlementaire, MandatSimple, MandatAvecSuppleant };

    internal class MandatTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MandatType) || t == typeof(MandatType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "MandatMission_Type":
                    return MandatType.MandatMission;
                case "MandatParlementaire_type":
                    return MandatType.MandatParlementaire;
                case "MandatSimple_Type":
                    return MandatType.MandatSimple;
                case "MandatAvecSuppleant_Type":
                    return MandatType.MandatAvecSuppleant;
            }
            throw new Exception("Cannot unmarshal type XsiType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MandatType)untypedValue;
            switch (value)
            {
                case MandatType.MandatMission:
                    serializer.Serialize(writer, "MandatMission_Type");
                    return;
                case MandatType.MandatParlementaire:
                    serializer.Serialize(writer, "MandatParlementaire_type");
                    return;
                case MandatType.MandatSimple:
                    serializer.Serialize(writer, "MandatSimple_Type");
                    return;
            }
            throw new Exception("Cannot marshal type XsiType");
        }

        public static readonly MandatTypeConverter Singleton = new MandatTypeConverter();
    }
}

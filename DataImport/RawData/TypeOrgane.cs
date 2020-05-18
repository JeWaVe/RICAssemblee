using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RICAssemblee.DataImport.RawData
{
    public enum TypeOrgane { 
        Hcj, 
        MisinfoPre, 
        Api, 
        Cjr, 
        Assemblee, 
        Senat, 
        DelegSenat, 
        ComSpSenat, 
        Gouvernement, 
        GroupeSenat, 
        ComSenat, 
        Ministere, 
        Cnps, 
        Confpt, 
        Cnpe, 
        Cmp, 
        Comper, 
        Comnl, 
        Gevi, 
        OffPar, 
        Deleg, 
        Ga, 
        Ge, 
        Gp, 
        MisInfo, 
        MisInfoCom, 
        OrgextParl, 
        Parpol, 
        DelegBureau 
    }

    internal class TypeOrganeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOrgane) || t == typeof(TypeOrgane?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "HCJ":
                    return TypeOrgane.Hcj;
                case "MISINFOPRE":
                    return TypeOrgane.MisinfoPre;
                case "ASSEMBLEE":
                    return TypeOrgane.Assemblee;
                case "SENAT":
                    return TypeOrgane.Senat;
                case "DELEGSENAT":
                    return TypeOrgane.DelegSenat;
                case "COMSPSENAT":
                    return TypeOrgane.ComSpSenat;
                case "GOUVERNEMENT":
                    return TypeOrgane.Gouvernement;
                case "GROUPESENAT":
                    return TypeOrgane.GroupeSenat;
                case "COMSENAT":
                    return TypeOrgane.ComSenat;
                case "MINISTERE":
                    return TypeOrgane.Ministere;
                case "MISINFOCOM":
                    return TypeOrgane.MisInfoCom;
                case "CNPS":
                    return TypeOrgane.Cnps;
                case "CNPE":
                    return TypeOrgane.Cnpe;
                case "CMP":
                    return TypeOrgane.Cmp;
                case "CONFPT":
                    return TypeOrgane.Confpt;
                case "CJR":
                    return TypeOrgane.Cjr;
                case "API":
                    return TypeOrgane.Api;
                case "COMPER":
                    return TypeOrgane.Comper;
                case "COMNL":
                    return TypeOrgane.Comnl;
                case "OFFPAR":
                    return TypeOrgane.OffPar;
                case "GEVI":
                    return TypeOrgane.Gevi;
                case "DELEG":
                    return TypeOrgane.Deleg;
                case "GA":
                    return TypeOrgane.Ga;
                case "GE":
                    return TypeOrgane.Ge;
                case "GP":
                    return TypeOrgane.Gp;
                case "MISINFO":
                    return TypeOrgane.MisInfo;
                case "ORGEXTPARL":
                    return TypeOrgane.OrgextParl;
                case "PARPOL":
                    return TypeOrgane.Parpol;
                case "DELEGBUREAU":
                    return TypeOrgane.DelegBureau;
            }
            throw new Exception("Cannot unmarshal type TypeOrgane");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeOrgane)untypedValue;
            switch (value)
            {
                case TypeOrgane.Assemblee:
                    serializer.Serialize(writer, "ASSEMBLEE");
                    return;
                case TypeOrgane.Cnps:
                    serializer.Serialize(writer, "CNPS");
                    return;
                case TypeOrgane.Comper:
                    serializer.Serialize(writer, "COMPER");
                    return;
                case TypeOrgane.Deleg:
                    serializer.Serialize(writer, "DELEG");
                    return;
                case TypeOrgane.Ga:
                    serializer.Serialize(writer, "GA");
                    return;
                case TypeOrgane.Ge:
                    serializer.Serialize(writer, "GE");
                    return;
                case TypeOrgane.Gp:
                    serializer.Serialize(writer, "GP");
                    return;
                case TypeOrgane.MisInfo:
                    serializer.Serialize(writer, "MISINFO");
                    return;
                case TypeOrgane.OrgextParl:
                    serializer.Serialize(writer, "ORGEXTPARL");
                    return;
                case TypeOrgane.Parpol:
                    serializer.Serialize(writer, "PARPOL");
                    return;
            }
            throw new Exception("Cannot marshal type TypeOrgane");
        }

        public static readonly TypeOrganeConverter Singleton = new TypeOrganeConverter();
    }
}

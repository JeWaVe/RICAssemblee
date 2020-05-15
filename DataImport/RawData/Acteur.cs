using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RICAssemblee.DataImport.RawData
{
    public class RawActeur
    {
        [JsonProperty("acteur", NullValueHandling = NullValueHandling.Ignore)]
        public Acteur Acteur { get; set; }
        public static RawActeur FromJson(string json) => JsonConvert.DeserializeObject<RawActeur>(json, Converter.Settings);
    }

    public class Acteur
    {
        [JsonProperty("@xmlns:xsi", NullValueHandling = NullValueHandling.Ignore)]
        public Uri XmlnsXsi { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public Uid Uid { get; set; }

        [JsonProperty("etatCivil", NullValueHandling = NullValueHandling.Ignore)]
        public EtatCivil EtatCivil { get; set; }

        [JsonProperty("profession", NullValueHandling = NullValueHandling.Ignore)]
        public Profession Profession { get; set; }

        [JsonProperty("uri_hatvp", NullValueHandling = NullValueHandling.Ignore)]
        public Uri UriHatvp { get; set; }

        [JsonProperty("adresses", NullValueHandling = NullValueHandling.Ignore)]
        public Adresses Adresses { get; set; }

        [JsonProperty("mandats", NullValueHandling = NullValueHandling.Ignore)]
        public Mandats Mandats { get; set; }
    }

    public class Adresses
    {
        [JsonProperty("adresse", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>[] Adresse { get; set; }
    }

    public class EtatCivil
    {
        [JsonProperty("ident", NullValueHandling = NullValueHandling.Ignore)]
        public Ident Ident { get; set; }

        [JsonProperty("infoNaissance", NullValueHandling = NullValueHandling.Ignore)]
        public InfoNaissance InfoNaissance { get; set; }

        [JsonProperty("dateDeces")]
        public object DateDeces { get; set; }
    }

    public class Ident
    {
        [JsonProperty("civ", NullValueHandling = NullValueHandling.Ignore)]
        public string Civ { get; set; }

        [JsonProperty("prenom", NullValueHandling = NullValueHandling.Ignore)]
        public string Prenom { get; set; }

        [JsonProperty("nom", NullValueHandling = NullValueHandling.Ignore)]
        public string Nom { get; set; }

        [JsonProperty("alpha", NullValueHandling = NullValueHandling.Ignore)]
        public string Alpha { get; set; }

        [JsonProperty("trigramme", NullValueHandling = NullValueHandling.Ignore)]
        public string Trigramme { get; set; }
    }

    public class InfoNaissance
    {
        [JsonProperty("dateNais", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateNais { get; set; }

        [JsonProperty("villeNais", NullValueHandling = NullValueHandling.Ignore)]
        public string VilleNais { get; set; }

        [JsonProperty("depNais", NullValueHandling = NullValueHandling.Ignore)]
        public string DepNais { get; set; }

        [JsonProperty("paysNais", NullValueHandling = NullValueHandling.Ignore)]
        public string PaysNais { get; set; }
    }

    public class Mandats
    {
        [JsonProperty("mandat", NullValueHandling = NullValueHandling.Ignore)]
        public Mandat[] Mandat { get; set; }
    }

    public class Mandat
    {
        [JsonProperty("@xsi:type", NullValueHandling = NullValueHandling.Ignore)]
        public XsiType? XsiType { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [JsonProperty("acteurRef", NullValueHandling = NullValueHandling.Ignore)]
        public Text? ActeurRef { get; set; }

        [JsonProperty("legislature")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Legislature { get; set; }

        [JsonProperty("typeOrgane", NullValueHandling = NullValueHandling.Ignore)]
        public TypeOrgane? TypeOrgane { get; set; }

        [JsonProperty("dateDebut", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateDebut { get; set; }

        [JsonProperty("datePublication")]
        public DateTimeOffset? DatePublication { get; set; }

        [JsonProperty("dateFin")]
        public DateTimeOffset? DateFin { get; set; }

        [JsonProperty("preseance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Preseance { get; set; }

        [JsonProperty("nominPrincipale", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NominPrincipale { get; set; }

        [JsonProperty("infosQualite", NullValueHandling = NullValueHandling.Ignore)]
        public InfosQualite InfosQualite { get; set; }

        [JsonProperty("organes", NullValueHandling = NullValueHandling.Ignore)]
        public Organes Organes { get; set; }

        [JsonProperty("suppleants", NullValueHandling = NullValueHandling.Ignore)]
        public Suppleants Suppleants { get; set; }

        [JsonProperty("chambre")]
        public object Chambre { get; set; }

        [JsonProperty("election", NullValueHandling = NullValueHandling.Ignore)]
        public Election Election { get; set; }

        [JsonProperty("mandature", NullValueHandling = NullValueHandling.Ignore)]
        public Mandature Mandature { get; set; }

        [JsonProperty("collaborateurs")]
        public Collaborateurs Collaborateurs { get; set; }

        [JsonProperty("libelle")]
        public object Libelle { get; set; }

        [JsonProperty("missionSuivanteRef")]
        public object MissionSuivanteRef { get; set; }

        [JsonProperty("missionPrecedenteRef")]
        public object MissionPrecedenteRef { get; set; }
    }

    public class Collaborateurs
    {
        [JsonProperty("collaborateur", NullValueHandling = NullValueHandling.Ignore)]
        public Collaborateur[] Collaborateur { get; set; }
    }

    public class Collaborateur
    {
        [JsonProperty("qualite", NullValueHandling = NullValueHandling.Ignore)]
        public string Qualite { get; set; }

        [JsonProperty("prenom", NullValueHandling = NullValueHandling.Ignore)]
        public string Prenom { get; set; }

        [JsonProperty("nom", NullValueHandling = NullValueHandling.Ignore)]
        public string Nom { get; set; }

        [JsonProperty("dateDebut")]
        public object DateDebut { get; set; }

        [JsonProperty("dateFin")]
        public object DateFin { get; set; }
    }

    public class Election
    {
        [JsonProperty("lieu", NullValueHandling = NullValueHandling.Ignore)]
        public Lieu Lieu { get; set; }

        [JsonProperty("causeMandat", NullValueHandling = NullValueHandling.Ignore)]
        public string CauseMandat { get; set; }

        [JsonProperty("refCirconscription", NullValueHandling = NullValueHandling.Ignore)]
        public string RefCirconscription { get; set; }
    }

    public class Lieu
    {
        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty("regionType", NullValueHandling = NullValueHandling.Ignore)]
        public string RegionType { get; set; }

        [JsonProperty("departement", NullValueHandling = NullValueHandling.Ignore)]
        public string Departement { get; set; }

        [JsonProperty("numDepartement", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NumDepartement { get; set; }

        [JsonProperty("numCirco", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NumCirco { get; set; }
    }

    public class InfosQualite
    {
        [JsonProperty("codeQualite", NullValueHandling = NullValueHandling.Ignore)]
        public Qualite? CodeQualite { get; set; }

        [JsonProperty("libQualite", NullValueHandling = NullValueHandling.Ignore)]
        public Qualite? LibQualite { get; set; }

        [JsonProperty("libQualiteSex", NullValueHandling = NullValueHandling.Ignore)]
        public LibQualiteSex? LibQualiteSex { get; set; }
    }

    public class Mandature
    {
        [JsonProperty("datePriseFonction", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DatePriseFonction { get; set; }

        [JsonProperty("causeFin")]
        public string CauseFin { get; set; }

        [JsonProperty("premiereElection", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PremiereElection { get; set; }

        [JsonProperty("placeHemicycle", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PlaceHemicycle { get; set; }

        [JsonProperty("mandatRemplaceRef")]
        public object MandatRemplaceRef { get; set; }
    }

    public class Organes
    {
        [JsonProperty("organeRef", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganeRef { get; set; }
    }

    public class Suppleants
    {
        [JsonProperty("suppleant", NullValueHandling = NullValueHandling.Ignore)]
        public Suppleant Suppleant { get; set; }
    }

    public class Suppleant
    {
        [JsonProperty("dateDebut", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateDebut { get; set; }

        [JsonProperty("dateFin")]
        public DateTimeOffset? DateFin { get; set; }

        [JsonProperty("suppleantRef", NullValueHandling = NullValueHandling.Ignore)]
        public string SuppleantRef { get; set; }
    }

    public class Profession
    {
        [JsonProperty("libelleCourant", NullValueHandling = NullValueHandling.Ignore)]
        public string LibelleCourant { get; set; }

        [JsonProperty("socProcINSEE", NullValueHandling = NullValueHandling.Ignore)]
        public SocProcInsee SocProcInsee { get; set; }
    }

    public class SocProcInsee
    {
        [JsonProperty("catSocPro", NullValueHandling = NullValueHandling.Ignore)]
        public string CatSocPro { get; set; }

        [JsonProperty("famSocPro", NullValueHandling = NullValueHandling.Ignore)]
        public string FamSocPro { get; set; }
    }

    public class Uid
    {
        [JsonProperty("@xsi:type", NullValueHandling = NullValueHandling.Ignore)]
        public string XsiType { get; set; }

        [JsonProperty("#text", NullValueHandling = NullValueHandling.Ignore)]
        public Text? Text { get; set; }
    }

    public enum Text { Pa441 };

    public enum Qualite { DéputéNonInscrit, Membre, MembreDeDroit, MembreDu, MembreSuppléant, MembreTitulaire, QualiteMembre, Secrétaire, Suppléant, Titulaire, VicePrésident };

    public enum LibQualiteSex { DéputéeNonInscrite, LibQualiteSexMembre, Membre, MembreDeDroit, MembreDu, MembreSuppléante, MembreTitulaire, Secrétaire, VicePrésidente };

    public enum TypeOrgane { Assemblee, Cnps, Comper, Deleg, Ga, Ge, Gp, Misinfo, Orgextparl, Parpol };

    public enum XsiType { MandatMissionType, MandatParlementaireType, MandatSimpleType };

    public static class Serialize
    {
        public static string ToJson(this RawActeur self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                XsiTypeConverter.Singleton,
                TextConverter.Singleton,
                QualiteConverter.Singleton,
                LibQualiteSexConverter.Singleton,
                TypeOrganeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class XsiTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(XsiType) || t == typeof(XsiType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "MandatMission_Type":
                    return XsiType.MandatMissionType;
                case "MandatParlementaire_type":
                    return XsiType.MandatParlementaireType;
                case "MandatSimple_Type":
                    return XsiType.MandatSimpleType;
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
            var value = (XsiType)untypedValue;
            switch (value)
            {
                case XsiType.MandatMissionType:
                    serializer.Serialize(writer, "MandatMission_Type");
                    return;
                case XsiType.MandatParlementaireType:
                    serializer.Serialize(writer, "MandatParlementaire_type");
                    return;
                case XsiType.MandatSimpleType:
                    serializer.Serialize(writer, "MandatSimple_Type");
                    return;
            }
            throw new Exception("Cannot marshal type XsiType");
        }

        public static readonly XsiTypeConverter Singleton = new XsiTypeConverter();
    }

    internal class TextConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Text) || t == typeof(Text?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "PA441")
            {
                return Text.Pa441;
            }
            throw new Exception("Cannot unmarshal type Text");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Text)untypedValue;
            if (value == Text.Pa441)
            {
                serializer.Serialize(writer, "PA441");
                return;
            }
            throw new Exception("Cannot marshal type Text");
        }

        public static readonly TextConverter Singleton = new TextConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class QualiteConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Qualite) || t == typeof(Qualite?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Député non-inscrit":
                    return Qualite.DéputéNonInscrit;
                case "Membre":
                    return Qualite.Membre;
                case "Membre de droit":
                    return Qualite.MembreDeDroit;
                case "Membre du":
                    return Qualite.MembreDu;
                case "Membre suppléant":
                    return Qualite.MembreSuppléant;
                case "Membre titulaire":
                    return Qualite.MembreTitulaire;
                case "Secrétaire":
                    return Qualite.Secrétaire;
                case "Suppléant":
                    return Qualite.Suppléant;
                case "Titulaire":
                    return Qualite.Titulaire;
                case "Vice-Président":
                    return Qualite.VicePrésident;
                case "membre":
                    return Qualite.QualiteMembre;
            }
            throw new Exception("Cannot unmarshal type Qualite");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Qualite)untypedValue;
            switch (value)
            {
                case Qualite.DéputéNonInscrit:
                    serializer.Serialize(writer, "Député non-inscrit");
                    return;
                case Qualite.Membre:
                    serializer.Serialize(writer, "Membre");
                    return;
                case Qualite.MembreDeDroit:
                    serializer.Serialize(writer, "Membre de droit");
                    return;
                case Qualite.MembreDu:
                    serializer.Serialize(writer, "Membre du");
                    return;
                case Qualite.MembreSuppléant:
                    serializer.Serialize(writer, "Membre suppléant");
                    return;
                case Qualite.MembreTitulaire:
                    serializer.Serialize(writer, "Membre titulaire");
                    return;
                case Qualite.Secrétaire:
                    serializer.Serialize(writer, "Secrétaire");
                    return;
                case Qualite.Suppléant:
                    serializer.Serialize(writer, "Suppléant");
                    return;
                case Qualite.Titulaire:
                    serializer.Serialize(writer, "Titulaire");
                    return;
                case Qualite.VicePrésident:
                    serializer.Serialize(writer, "Vice-Président");
                    return;
                case Qualite.QualiteMembre:
                    serializer.Serialize(writer, "membre");
                    return;
            }
            throw new Exception("Cannot marshal type Qualite");
        }

        public static readonly QualiteConverter Singleton = new QualiteConverter();
    }

    internal class LibQualiteSexConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LibQualiteSex) || t == typeof(LibQualiteSex?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Députée non-inscrite":
                    return LibQualiteSex.DéputéeNonInscrite;
                case "Membre":
                    return LibQualiteSex.Membre;
                case "Membre de droit":
                    return LibQualiteSex.MembreDeDroit;
                case "Membre du":
                    return LibQualiteSex.MembreDu;
                case "Membre suppléante":
                    return LibQualiteSex.MembreSuppléante;
                case "Membre titulaire":
                    return LibQualiteSex.MembreTitulaire;
                case "Secrétaire":
                    return LibQualiteSex.Secrétaire;
                case "Vice-Présidente":
                    return LibQualiteSex.VicePrésidente;
                case "membre":
                    return LibQualiteSex.LibQualiteSexMembre;
            }
            throw new Exception("Cannot unmarshal type LibQualiteSex");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (LibQualiteSex)untypedValue;
            switch (value)
            {
                case LibQualiteSex.DéputéeNonInscrite:
                    serializer.Serialize(writer, "Députée non-inscrite");
                    return;
                case LibQualiteSex.Membre:
                    serializer.Serialize(writer, "Membre");
                    return;
                case LibQualiteSex.MembreDeDroit:
                    serializer.Serialize(writer, "Membre de droit");
                    return;
                case LibQualiteSex.MembreDu:
                    serializer.Serialize(writer, "Membre du");
                    return;
                case LibQualiteSex.MembreSuppléante:
                    serializer.Serialize(writer, "Membre suppléante");
                    return;
                case LibQualiteSex.MembreTitulaire:
                    serializer.Serialize(writer, "Membre titulaire");
                    return;
                case LibQualiteSex.Secrétaire:
                    serializer.Serialize(writer, "Secrétaire");
                    return;
                case LibQualiteSex.VicePrésidente:
                    serializer.Serialize(writer, "Vice-Présidente");
                    return;
                case LibQualiteSex.LibQualiteSexMembre:
                    serializer.Serialize(writer, "membre");
                    return;
            }
            throw new Exception("Cannot marshal type LibQualiteSex");
        }

        public static readonly LibQualiteSexConverter Singleton = new LibQualiteSexConverter();
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
                case "ASSEMBLEE":
                    return TypeOrgane.Assemblee;
                case "CNPS":
                    return TypeOrgane.Cnps;
                case "COMPER":
                    return TypeOrgane.Comper;
                case "DELEG":
                    return TypeOrgane.Deleg;
                case "GA":
                    return TypeOrgane.Ga;
                case "GE":
                    return TypeOrgane.Ge;
                case "GP":
                    return TypeOrgane.Gp;
                case "MISINFO":
                    return TypeOrgane.Misinfo;
                case "ORGEXTPARL":
                    return TypeOrgane.Orgextparl;
                case "PARPOL":
                    return TypeOrgane.Parpol;
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
                case TypeOrgane.Misinfo:
                    serializer.Serialize(writer, "MISINFO");
                    return;
                case TypeOrgane.Orgextparl:
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

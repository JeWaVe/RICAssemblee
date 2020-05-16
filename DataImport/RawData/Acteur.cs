using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

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

        [JsonProperty("uid")]
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
        public MandatType? XsiType { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [JsonProperty("acteurRef", NullValueHandling = NullValueHandling.Ignore)]
        public string ActeurRef { get; set; }

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
        [JsonConverter(typeof(CollaborateurConverter<Collaborateur>))]
        public IEnumerable<Collaborateur> Collaborateur { get; set; }


        internal class CollaborateurConverter<T> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(List<T>));
            }

            public override object ReadJson(
              JsonReader reader,
              Type objectType,
              object existingValue,
              JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                    return token.ToObject<List<T>>();
                return new List<T> { token.ToObject<T>() };
            }

            public override bool CanWrite
            {
                get
                {
                    return false;
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
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
        [JsonConverter(typeof(OrganeRefConverter<string>))]
        public IEnumerable<string> OrganeRef { get; set; }

        internal class OrganeRefConverter<T> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(List<T>));
            }

            public override object ReadJson(
              JsonReader reader,
              Type objectType,
              object existingValue,
              JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                    return token.ToObject<List<T>>();
                return new List<T> { token.ToObject<T>() };
            }

            public override bool CanWrite
            {
                get
                {
                    return false;
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
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

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public enum Qualite
    { 
        DeputeNonInscrit, 
        Senateur,
        Questeur,
        Ministre,
        EnMission,
        AutreMembreDuBureau,
        SecretaireDeLAssembleeNationale, 
        VicePresidentDeLAssembleeNationale, 
        CoRapporteur,
        JugeTitulaire,
        JugeSuppleant,
        RapporteurBudgetairePourAvis, 
        Rapporteur, 
        RapporteurSpecial,
        RapporteurGeneral, 
        RapporteurThematique,
        Membre, 
        MembreDeDroit, 
        MembreDeDroitDuBureau, 
        MembreApparente, 
        MembreSuppleant, 
        MembreDesigneParLesGroupes, 
        MembreTitulaire, 
        QualiteMembre, 
        Secretaire,
        SecretaireGeneralAdjoint,
        Suppleant, 
        Titulaire, 
        VicePresident,
        PremierVicePresident,
        CoPresident,
        SecretaireDEtat,
        TresorierAdjoint,
        RepresentantDuPresidentDeGroupe,
        President,
        PresidentDeDroit,
        PresidentDelegue,
        PresidentExecutif,
        PresidentDAge,
        SecretaireDAge,
        PresidentDeGroupe 
    };

    public enum TypeOrgane { MisinfoPre, Api, Cjr, Assemblee, Senat, DelegSenat, ComSpSenat, Gouvernement, GroupeSenat, ComSenat, Ministere, Cnps, Confpt, Cnpe, Cmp, Comper, Comnl, Gevi, OffPar, Deleg, Ga, Ge, Gp, MisInfo, MisInfoCom, Orgextparl, Parpol, DelegBureau };

    public enum MandatType { MandatMission, MandatParlementaire, MandatSimple, MandatAvecSuppleant };

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
                QualiteConverter.Singleton,
                TypeOrganeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class XsiTypeConverter : JsonConverter
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

        public static readonly XsiTypeConverter Singleton = new XsiTypeConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out l))
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
            switch (value.ToLowerInvariant())
            {
                case "autre membre du bureau":
                    return Qualite.AutreMembreDuBureau;
                case "questeur":
                    return Qualite.Questeur;
                case "député non-inscrit":
                    return Qualite.DeputeNonInscrit;
                case "sénateur":
                    return Qualite.Senateur;
                case "ministre délégué":
                case "ministre d'état":
                case "ministre":
                    return Qualite.Ministre;
                case "en mission":
                    return Qualite.EnMission;
                case "secrétaire de l'assemblée nationale":
                    return Qualite.SecretaireDeLAssembleeNationale;
                case "vice-président de l'assemblée nationale":
                    return Qualite.VicePresidentDeLAssembleeNationale;
                case "rapporteur budgétaire pour avis":
                    return Qualite.RapporteurBudgetairePourAvis;
                case "juge titulaire":
                    return Qualite.JugeTitulaire;
                case "juge suppléant":
                    return Qualite.JugeSuppleant;
                case "co-rapporteur":
                    return Qualite.CoRapporteur;
                case "rapporteur":
                    return Qualite.Rapporteur;
                case "rapporteur spécial au nom de la commission des finances sur le budget de l'agriculture et de la pêche (agriculture)":
                case "rapporteur spécial au nom de la commission des finances : agriculture, pêche, forêt et affaires rurales; développement agricole et rural":
                case "rapporteur spécial au nom de la commission des finances : agriculture, pêche, forêt, et affaires rurales; développement agricole et rural":
                case "rapporteur spécial au nom de la commission des finances : administration générale et territoriale de l'état":
                case "rapporteur spécial":
                    return Qualite.RapporteurSpecial;
                case "rapporteur thématique":
                    return Qualite.RapporteurThematique;
                case "rapporteur général":
                    return Qualite.RapporteurGeneral;
                case "membre":
                case "membre rattaché":
                case "membre nommé par le président de l'assemblée nationale":
                case "membre du":
                    return Qualite.Membre;
                case "membre de droit":
                case "membre de droit (représentant de la commission des finances)":
                case "membre de droit (représentant de la commission des affaires étrangères)":
                case "membre de droit(rapporteur du projet de loi de financement de la séc.sociale)":
                case "membre de droit (rapporteur du projet de loi de financement de la séc. sociale)":
                case "membre de droit (président de la commission de la défense)":
                case "membre de droit (président de la commission des lois)":
                case "membre de droit (représentante de la commission des affaires culturelles)":
                    return Qualite.MembreDeDroit;
                case "membre de droit du bureau":
                    return Qualite.MembreDeDroitDuBureau;
                case "membre apparenté":
                    return Qualite.MembreApparente;
                case "membre suppléant":
                    return Qualite.MembreSuppleant;
                case "membre désigné par les groupes":
                    return Qualite.MembreDesigneParLesGroupes;
                case "membre titulaire":
                    return Qualite.MembreTitulaire;
                case "secrétaire":
                    return Qualite.Secretaire;
                case "secrétaire général-adjoint":
                    return Qualite.SecretaireGeneralAdjoint;
                case "président de droit":
                    return Qualite.PresidentDeDroit;
                case "suppléant":
                    return Qualite.Suppleant;
                case "titulaire":
                    return Qualite.Titulaire;
                case "vice-président, co-rapporteur": // forget about co-rapporteur in that case
                case "vice-président":
                case "deuxième vice-président":
                    return Qualite.VicePresident;
                case "premier vice-président":
                    return Qualite.PremierVicePresident;
                case "président":
                case "président-rapporteur": // forget about rapporteur when president
                case "président du":
                    return Qualite.President;
                case "président de groupe":
                    return Qualite.PresidentDeGroupe;
                case "secrétaire d'âge":
                    return Qualite.SecretaireDAge;
                case "président délégué":
                    return Qualite.PresidentDelegue;
                case "président exécutif":
                    return Qualite.PresidentExecutif;
                case "secrétaire d'état":
                    return Qualite.SecretaireDEtat;
                case "trésorier adjoint":
                    return Qualite.TresorierAdjoint;
                case "représentant du président de groupe":
                    return Qualite.RepresentantDuPresidentDeGroupe;
                case "président d'âge":
                    return Qualite.PresidentDAge;
                case "co-président":
                    return Qualite.CoPresident;
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
                case Qualite.DeputeNonInscrit:
                    serializer.Serialize(writer, "Député non-inscrit");
                    return;
                case Qualite.Membre:
                    serializer.Serialize(writer, "Membre");
                    return;
                case Qualite.MembreDeDroit:
                    serializer.Serialize(writer, "Membre de droit");
                    return;
                case Qualite.MembreSuppleant:
                    serializer.Serialize(writer, "Membre suppléant");
                    return;
                case Qualite.MembreTitulaire:
                    serializer.Serialize(writer, "Membre titulaire");
                    return;
                case Qualite.Secretaire:
                    serializer.Serialize(writer, "Secrétaire");
                    return;
                case Qualite.Suppleant:
                    serializer.Serialize(writer, "Suppléant");
                    return;
                case Qualite.Titulaire:
                    serializer.Serialize(writer, "Titulaire");
                    return;
                case Qualite.VicePresident:
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

    internal class TypeOrganeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOrgane) || t == typeof(TypeOrgane?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
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
                    return TypeOrgane.Orgextparl;
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

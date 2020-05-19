using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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
        public Adresse[] Adresse { get; set; }
    }

    public class Adresse
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(AdresseTypeConverter))]
        public AdresseType Type { get; set; }

        [JsonProperty("adresseDeRattachement")]
        public string AdresseDeRattachement { get; set; }

        [JsonProperty("valElec", NullValueHandling = NullValueHandling.Ignore)]
        public string ValElec { get; set; }

        [JsonProperty("intitule", NullValueHandling = NullValueHandling.Ignore)]
        public string Intitule { get; set; }

        [JsonProperty("numeroRue", NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroRue { get; set; }

        [JsonProperty("nomRue", NullValueHandling = NullValueHandling.Ignore)]
        public string NomRue { get; set; }

        [JsonProperty("complementAdresse", NullValueHandling = NullValueHandling.Ignore)]
        public string ComplementAdresse { get; set; }

        [JsonProperty("codePostal", NullValueHandling = NullValueHandling.Ignore)]
        public string CodePostal { get; set; }

        [JsonProperty("ville", NullValueHandling = NullValueHandling.Ignore)]
        public string Ville { get; set; }
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
        [JsonProperty("dateNais")]
        public DateTimeOffset DateNais { get; set; }

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

        // always equal to acteur.Uid.Text
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
        public string Libelle { get; set; }

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


        public class CollaborateurConverter<T> : JsonConverter
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
        [JsonProperty("codeQualite")]
        public Qualite CodeQualite { get; set; }
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

        public class OrganeRefConverter<T> : JsonConverter
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

    internal static class Serialize
    {
        public static string ToJson(this RawActeur self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}

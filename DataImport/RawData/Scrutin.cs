﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace RICAssemblee.DataImport.RawData
{
    internal class RawScrutin
    {
        [JsonProperty("scrutin", NullValueHandling = NullValueHandling.Ignore)]
        public Scrutin Scrutin { get; set; }

        public static RawScrutin FromJson(string json) => JsonConvert.DeserializeObject<RawScrutin>(json, Converter.Settings);

        public static IEnumerable<Scrutin> FromDirectory(string dir) {
            var result = new List<Scrutin>();
            foreach (var f in Directory.GetFiles(dir))
            {
                result.Add(FromJson(File.ReadAllText(f)).Scrutin);
            }

            return result;
        }
    }

    internal class Scrutin : BaseRawData
    {
        [JsonIgnore]
        public override string Uid { get { return SessionRef; } set { throw new Exception("cannot set scrutin Uid"); } }

        [JsonProperty("@xmlns", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Xmlns { get; set; }

        [JsonProperty("@xmlns:xsi", NullValueHandling = NullValueHandling.Ignore)]
        public Uri XmlnsXsi { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Numero { get; set; }

        [JsonProperty("organeRef", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganeRef { get; set; }

        [JsonProperty("legislature", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Legislature { get; set; }

        [JsonProperty("sessionRef", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionRef { get; set; }

        [JsonProperty("seanceRef", NullValueHandling = NullValueHandling.Ignore)]
        public string SeanceRef { get; set; }

        [JsonProperty("dateScrutin", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateScrutin { get; set; }

        [JsonProperty("quantiemeJourSeance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? QuantiemeJourSeance { get; set; }

        [JsonProperty("typeVote", NullValueHandling = NullValueHandling.Ignore)]
        public TypeVote TypeVote { get; set; }

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public Sort Sort { get; set; }

        [JsonProperty("titre", NullValueHandling = NullValueHandling.Ignore)]
        public string Titre { get; set; }

        [JsonProperty("demandeur", NullValueHandling = NullValueHandling.Ignore)]
        public Demandeur Demandeur { get; set; }

        [JsonProperty("objet", NullValueHandling = NullValueHandling.Ignore)]
        public Objet Objet { get; set; }

        [JsonProperty("modePublicationDesVotes", NullValueHandling = NullValueHandling.Ignore)]
        public string ModePublicationDesVotes { get; set; }

        [JsonProperty("syntheseVote", NullValueHandling = NullValueHandling.Ignore)]
        public SyntheseVote SyntheseVote { get; set; }

        [JsonProperty("ventilationVotes", NullValueHandling = NullValueHandling.Ignore)]
        public VentilationVotes VentilationVotes { get; set; }

        // ugly raw type from raw data : to be investigated
        //[JsonProperty("miseAuPoint")]
        //public MiseAuPoint MiseAuPoint { get; set; }
    }

    internal class Demandeur
    {
        [JsonProperty("texte", NullValueHandling = NullValueHandling.Ignore)]
        public string Texte { get; set; }

        [JsonProperty("referenceLegislative")]
        public object ReferenceLegislative { get; set; }
    }

    internal class MiseAuPoint
    {
        [JsonProperty("nonVotants", NullValueHandling = NullValueHandling.Ignore)]
        public VotantWrapper NonVotants { get; set; }

        [JsonProperty("pours", NullValueHandling = NullValueHandling.Ignore)]
        public VotantWrapper Pours { get; set; }

        [JsonProperty("abstentions", NullValueHandling = NullValueHandling.Ignore)]
        public VotantWrapper Abstentions { get; set; }

        [JsonProperty("nonVotantsVolontaires", NullValueHandling = NullValueHandling.Ignore)]
        public VotantWrapper NonVotantsVolontaires { get; set; }

        [JsonProperty("contres", NullValueHandling = NullValueHandling.Ignore)]
        public VotantWrapper Contres { get; set; }

        [JsonProperty("dysfonctionnement", NullValueHandling = NullValueHandling.Ignore)]
        public Decompte Dysfonctionnement { get; set; }
    }

    internal class Decompte
    {
        [JsonProperty("nonVotants")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NonVotants { get; set; }

        [JsonProperty("pour")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Pour { get; set; }

        [JsonProperty("contre")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Contre { get; set; }

        [JsonProperty("abstentions")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Abstentions { get; set; }

        [JsonProperty("nonVotantsVolontaires")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NonVotantsVolontaires { get; set; }
    }

    internal class Objet
    {
        [JsonProperty("libelle", NullValueHandling = NullValueHandling.Ignore)]
        public string Libelle { get; set; }

        [JsonProperty("referenceLegislative")]
        public object ReferenceLegislative { get; set; }
    }

    internal class Sort
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("libelle", NullValueHandling = NullValueHandling.Ignore)]
        public string Libelle { get; set; }
    }

    internal class SyntheseVote
    {
        [JsonProperty("nombreVotants", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NombreVotants { get; set; }

        [JsonProperty("suffragesExprimes", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SuffragesExprimes { get; set; }

        [JsonProperty("nbrSuffragesRequis", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NbrSuffragesRequis { get; set; }

        [JsonProperty("annonce", NullValueHandling = NullValueHandling.Ignore)]
        public string Annonce { get; set; }

        [JsonProperty("decompte", NullValueHandling = NullValueHandling.Ignore)]
        public Decompte Decompte { get; set; }
    }

    internal class TypeVote
    {
        [JsonProperty("codeTypeVote", NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypeVote { get; set; }

        [JsonProperty("libelleTypeVote", NullValueHandling = NullValueHandling.Ignore)]
        public string LibelleTypeVote { get; set; }

        [JsonProperty("typeMajorite", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeMajorite { get; set; }
    }

    internal class VentilationVotes
    {
        [JsonProperty("organe", NullValueHandling = NullValueHandling.Ignore)]
        public OrganeScrutin OrganeScrutin { get; set; }
    }

    internal class OrganeScrutin
    {
        [JsonProperty("organeRef", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganeRef { get; set; }

        [JsonProperty("groupes", NullValueHandling = NullValueHandling.Ignore)]
        public Groupes Groupes { get; set; }
    }

    internal class Groupes
    {
        [JsonProperty("groupe", NullValueHandling = NullValueHandling.Ignore)]
        public Groupe[] Groupe { get; set; }
    }

    internal class Groupe
    {
        [JsonProperty("organeRef", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganeRef { get; set; }

        [JsonProperty("nombreMembresGroupe", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NombreMembresGroupe { get; set; }

        [JsonProperty("vote", NullValueHandling = NullValueHandling.Ignore)]
        public Vote Vote { get; set; }
    }

    internal class Vote
    {
        [JsonProperty("positionMajoritaire", NullValueHandling = NullValueHandling.Ignore)]
        public PositionMajoritaire? PositionMajoritaire { get; set; }

        [JsonProperty("decompteVoix", NullValueHandling = NullValueHandling.Ignore)]
        public Decompte DecompteVoix { get; set; }

        [JsonProperty("decompteNominatif", NullValueHandling = NullValueHandling.Ignore)]
        public DecompteNominatif DecompteNominatif { get; set; }
    }

    internal class DecompteNominatif
    {
        [JsonProperty("pours")]
        public VotantWrapper Pours { get; set; }

        [JsonProperty("contres")]
        public VotantWrapper Contres { get; set; }

        [JsonProperty("abstentions")]
        public VotantWrapper Abstentions { get; set; }

        [JsonProperty("nonVotants")]
        public VotantWrapper NonVotants { get; set; }
    }

    internal class VotantWrapper
    {
        [JsonProperty("votant")]
        [JsonConverter(typeof(ItemOrArrayConverter<Votant>))]
        public IEnumerable<Votant> Votant { get; set; }
    }

    internal class Votant
    {
        [JsonProperty("acteurRef", NullValueHandling = NullValueHandling.Ignore)]
        public string ActeurRef { get; set; }

        [JsonProperty("mandatRef", NullValueHandling = NullValueHandling.Ignore)]
        public string MandatRef { get; set; }

        [JsonProperty("parDelegation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ParDelegation { get; set; }

        [JsonProperty("causePositionVote", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CausePositionVoteConverter))]
        public CausePositionVote CausePositionVote { get; set; }
    }

    public enum CausePositionVote { MembreGouvernement, PresidentAssembleeNationale, PresidentSceance, PositionPersonnelle };

    public enum PositionMajoritaire { Abstention, Contre, Pour };

    internal class CausePositionVoteConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CausePositionVote) || t == typeof(CausePositionVote?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "MG":
                    return CausePositionVote.MembreGouvernement;
                case "PAN":
                    return CausePositionVote.PresidentAssembleeNationale;
                case "PSE":
                    return CausePositionVote.PresidentSceance;
                default:
                    return CausePositionVote.PositionPersonnelle;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CausePositionVote)untypedValue;
            switch (value)
            {
                case CausePositionVote.MembreGouvernement:
                    serializer.Serialize(writer, "MG");
                    return;
                case CausePositionVote.PresidentAssembleeNationale:
                    serializer.Serialize(writer, "PAN");
                    return;
                case CausePositionVote.PresidentSceance:
                    serializer.Serialize(writer, "PSE");
                    return;
                case CausePositionVote.PositionPersonnelle:
                default:
                    serializer.Serialize(writer, "PP");
                    return;
            }
        }

        public static readonly CausePositionVoteConverter Singleton = new CausePositionVoteConverter();
    }

    internal class PositionMajoritaireConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PositionMajoritaire) || t == typeof(PositionMajoritaire?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "abstention":
                    return PositionMajoritaire.Abstention;
                case "contre":
                    return PositionMajoritaire.Contre;
                case "pour":
                    return PositionMajoritaire.Pour;
            }
            throw new Exception("Cannot unmarshal type PositionMajoritaire");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PositionMajoritaire)untypedValue;
            switch (value)
            {
                case PositionMajoritaire.Abstention:
                    serializer.Serialize(writer, "abstention");
                    return;
                case PositionMajoritaire.Contre:
                    serializer.Serialize(writer, "contre");
                    return;
                case PositionMajoritaire.Pour:
                    serializer.Serialize(writer, "pour");
                    return;
            }
            throw new Exception("Cannot marshal type PositionMajoritaire");
        }

        public static readonly PositionMajoritaireConverter Singleton = new PositionMajoritaireConverter();
    }
}
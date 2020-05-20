using Newtonsoft.Json;
using System;

namespace RICAssemblee.DataImport.RawData
{
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
}

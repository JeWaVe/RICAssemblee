using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class ScrutinModel : BaseModel
    {
        public string Titre { get; set; }

        public string Demandeur { get; set; }

        public DateTimeOffset Date { get; set; }

        public List<GroupeResult> Results { get; set; }

        public string Annonce { get; set; }

        public int Numero { get; set; }

        public Vote GetVote(DeputeModel depute)
        {
            foreach (var groupe in Results)
            {
                if (groupe.Pour.FirstOrDefault(v => v.Depute.Equals(depute)) != null)
                    return Vote.Pour;
                if (groupe.Abstentions.FirstOrDefault(v => v.Depute.Equals(depute)) != null)
                    return Vote.Abstention;
                if (groupe.Contre.FirstOrDefault(v => v.Depute.Equals(depute)) != null)
                    return Vote.Contre;
                if (groupe.NonVotants.FirstOrDefault(v => v.Depute.Equals(depute)) != null)
                    return Vote.NonVotant;
            }

            return Vote.Inconnu;
        }

        public enum Vote
        {
            Abstention, 
            NonVotant, 
            Pour, 
            Contre, 
            Inconnu
        }

        public int Pour => new Lazy<int>(() => Results.Aggregate(0, (accum, g) => g.Pour.Count() + accum)).Value;
        public int Contre => new Lazy<int>(() => Results.Aggregate(0, (accum, g) => g.Contre.Count() + accum)).Value;
        public int Abstentions => new Lazy<int>(() => Results.Aggregate(0, (accum, g) => g.Abstentions.Count() + accum)).Value;
        public int NonVotants => new Lazy<int>(() => Results.Aggregate(0, (accum, g) => g.NonVotants.Count() + accum)).Value;

        public int TotalExprimes { get; set; }

        internal ScrutinModel(Scrutin rawScrutin)
        {
            this.Uid = rawScrutin.Uid;
            this.Numero = (int) rawScrutin.Numero;
            this.Titre = rawScrutin.Titre;
            this.Demandeur = rawScrutin.Demandeur.Texte;
            this.Date = rawScrutin.DateScrutin.GetValueOrDefault();
            this.Annonce = rawScrutin.SyntheseVote.Annonce;
            this.Results = new List<GroupeResult>();
            this.TotalExprimes = (int) rawScrutin.SyntheseVote.SuffragesExprimes;
            foreach (var groupe in rawScrutin.VentilationVotes.OrganeScrutin.Groupes.Groupe)
            {
                // forge new gp as it may have changed 
                GroupeParlementaireModel groupeParlementaire = new GroupeParlementaireModel(ObjectStorage<BaseRawData>.Singleton().Get<Organe>(groupe.OrganeRef));

                this.Results.Add(new GroupeResult
                {
                    Abstentions = ParseVotants(groupeParlementaire, groupe.Vote.DecompteNominatif.Abstentions?.Votant),
                    NonVotants = ParseVotants(groupeParlementaire, groupe.Vote.DecompteNominatif.NonVotants?.Votant),
                    Pour = ParseVotants(groupeParlementaire, groupe.Vote.DecompteNominatif.Pours?.Votant),
                    Contre = ParseVotants(groupeParlementaire, groupe.Vote.DecompteNominatif.Contres?.Votant),
                    Groupe = groupeParlementaire
                });
            }

            _modelStorage.Register(this.Uid, this);
        }

        private HashSet<VoteDepute> ParseVotants(GroupeParlementaireModel gp, IEnumerable<Votant> votants)
        {
            if (votants == null)
                return new HashSet<VoteDepute>();
            return new HashSet<VoteDepute>(votants
                .Where(v => _modelStorage.Contains<DeputeModel>(v.ActeurRef)) // somehow unknown acteurRef appears here : to be reported to questeur
                .Select(v =>
                {
                    var depute = _modelStorage.Get<DeputeModel>(v.ActeurRef);
                    gp.Deputes.Add(depute);
                    return new VoteDepute
                    {
                        Depute = depute,
                        ParDelegation = v.ParDelegation.HasValue ? v.ParDelegation.Value : false,
                        CausePositionVote = v.CausePositionVote
                    };
                }));
        }

        public class GroupeResult
        {
            public Vote PositionMajoritaire 
            { 
                get
                {
                    if (Pour.Count > Contre.Count && Pour.Count > Abstentions.Count && Pour.Count > NonVotants.Count)
                        return Vote.Pour;
                    if (Contre.Count > Pour.Count && Contre.Count > Abstentions.Count && Contre.Count > NonVotants.Count)
                        return Vote.Contre;
                    if (Abstentions.Count > Contre.Count && Abstentions.Count > Pour.Count && Abstentions.Count > NonVotants.Count)
                        return Vote.Pour;
                    if (NonVotants.Count > Contre.Count && NonVotants.Count > Abstentions.Count && NonVotants.Count > Pour.Count)
                        return Vote.NonVotant;
                    return Vote.Inconnu;
                } 
            }

            public GroupeParlementaireModel Groupe { get; set; }

            public HashSet<VoteDepute> NonVotants { get; set; }

            public HashSet<VoteDepute> Abstentions { get; set; }

            public HashSet<VoteDepute> Pour { get; set; }

            public HashSet<VoteDepute> Contre { get; set; }
        }


        public class VoteDepute
        {
            public DeputeModel Depute { get; set; }

            public bool ParDelegation { get; set; }

            public CausePositionVote CausePositionVote { get; set; }

            public override bool Equals(object obj)
            {
                return this.Depute.Equals(obj);
            }

            public override int GetHashCode()
            {
                return this.Depute.GetHashCode();
            }
        }
    }
}

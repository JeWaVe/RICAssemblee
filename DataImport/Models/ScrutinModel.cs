using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class ScrutinModel : BaseModel
    {
        public string Titre { get; set; }

        public List<GroupeResult> Results { get; set; }

        internal ScrutinModel(Scrutin rawScrutin) 
        {
            this.Uid = rawScrutin.Uid;
            this.Titre = rawScrutin.Titre;
            this.Results = new List<GroupeResult>();
            foreach(var groupe in rawScrutin.VentilationVotes.OrganeScrutin.Groupes.Groupe)
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
        }

        private List<DeputeModel> ParseVotants(GroupeParlementaireModel gp, IEnumerable<Votant> votants)
        {
            if (votants == null)
                return new List<DeputeModel>();
            return votants
                .Where(v => _modelStorage.Contains<DeputeModel>(v.ActeurRef)) // somehow unknown acteurRef appears here : to be reported to questeur
                .Select(v =>
                {
                    var result = _modelStorage.Get<DeputeModel>(v.ActeurRef);
                    gp.Deputes.Add(result);
                    return result;
                })
                .ToList();
        }

        public class GroupeResult
        {
            public GroupeParlementaireModel Groupe { get; set; }

            public List<DeputeModel> NonVotants { get; set; }

            public List<DeputeModel> Abstentions { get; set; }

            public List<DeputeModel> Pour { get; set; }

            public List<DeputeModel> Contre { get; set; }
        }
    }
}

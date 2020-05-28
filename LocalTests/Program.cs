using RICAssemblee.DataImport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");
            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");

            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/loi/scrutins/Scrutins_XV.json.zip", "scrutins.zip");
            //ZipFile.ExtractToDirectory("scrutins.zip", "scrutins");

            var factory = new ModelFactory();
            string parentDir = Path.Combine(Environment.CurrentDirectory, "acteurs");
            var organes = factory.Organes(parentDir);
            var deputes = factory.Deputes(parentDir);

            var scrutins = factory.Scrutins(Path.Combine(Environment.CurrentDirectory, "scrutins", "json"))
                .OrderByDescending(s => s.TotalExprimes).ToList();

            // recupère les votes "dissidents" du groupe eds pour voir s'ils ont voté contre lrem de temps en temps
            var eds = ObjectStorage<BaseModel>.Singleton().Get<GroupeParlementaireModel>("po771789");
            
            Dictionary<DeputeModel, List<ScrutinModel>> res = new Dictionary<DeputeModel, List<ScrutinModel>>();
            foreach(var depute in eds.Deputes)
            {
                foreach (var scrutin in scrutins)
                {
                    var vote = scrutin.GetVote(depute);

                    foreach (var result in scrutin.Results)
                    {
                        if(result.Groupe.Nom.ToLower().Contains("marche") && result.Groupe.Deputes.Contains(depute)) 
                        {
                            if(!res.ContainsKey(depute))
                            {
                                res.Add(depute, new List<ScrutinModel>());
                            }
                            if(result.PositionMajoritaire != vote)
                            {
                                res[depute].Add(scrutin);
                            }
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            List<string> csv = new List<string>();
            foreach (var (d, s) in res)
            {
                csv.Add(string.Format($"{d.Prenom} {d.Nom},{s.Count}"));
                csv.Add(string.Format($",position personnelle, position LREM,titre"));
                sb.Append(d.Prenom).Append(" ").Append(d.Nom).Append(" : ").AppendLine();
                foreach(var sc in s)
                {
                    
                    var lremVote = sc.Results.First(g => g.Groupe.Nom.Contains("Marche")).PositionMajoritaire;
                    sb.Append("\tAu scrutin portant sur ").Append(sc.Titre).AppendLine();
                    sb.Append('\t').Append("a voté : ");
                    var vote = sc.GetVote(d);
                    sb.Append(vote.ToString()).AppendLine().Append("\t\talors que #LREM a voté : ");
                    sb.Append(lremVote).AppendLine();
                    csv.Add(string.Format($",{vote},{lremVote},{sc.Titre}"));
                }

            }
            File.WriteAllLines("eds.csv", csv);
            File.WriteAllText("eds.txt", sb.ToString());
        }
    }
}

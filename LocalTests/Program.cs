using RICAssemblee.DataImport.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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


#if false // permanences parlementaires
            var addresses = deputes.Where(d => d.Adresses.FirstOrDefault(a => a.Type == RICAssemblee.DataImport.RawData.AdresseType.Circonscription) != null)
                .Select(d => (
                        d.Adresses.First(add => add.Type == RICAssemblee.DataImport.RawData.AdresseType.Circonscription) as AdressePostaleModel,
                        string.Format($"{d.Prenom.Replace(',', ' ')} {d.Nom.Replace(',', ' ')}")))
                .Select(item => String.Format($"{item.Item1.NumeroRue?.Replace(',', ' ')} {item.Item1.NomRue?.Replace(',', ' ')} {item.Item1.CodePostal} {item.Item1.Ville}, {item.Item2}"));
            File.WriteAllLines("addresses.csv", addresses, Encoding.UTF8);
            return;
#endif
            // recupère les votes "dissidents" du groupe eds pour voir s'ils ont voté contre lrem de temps en temps
            var eds = ObjectStorage<BaseModel>.Singleton().Get<GroupeParlementaireModel>("po771789");
            
            Dictionary<DeputeModel, List<ScrutinModel>> res = new Dictionary<DeputeModel, List<ScrutinModel>>();
            foreach(var depute in eds.Deputes)
            {
                foreach (var scrutin in scrutins)
                {
                    var vote = scrutin.GetVote(depute);
                    if (vote != ScrutinModel.Vote.NonVotant && vote != ScrutinModel.Vote.Abstention)
                    {
                        foreach (var result in scrutin.Results)
                        {
                            if (result.Groupe.Nom.ToLower().Contains("marche") && result.Groupe.Deputes.Contains(depute))
                            {
                                if (!res.ContainsKey(depute))
                                {
                                    res.Add(depute, new List<ScrutinModel>());
                                }
                                if (result.PositionMajoritaire != vote)
                                {
                                    res[depute].Add(scrutin);
                                }
                            }
                        }
                    }
                }
            }

            List<string> csv = new List<string>();
            Dictionary<DateTimeOffset, int> votesPerDate = new Dictionary<DateTimeOffset, int>();
            Dictionary<int, int> votesPerScrutin = new Dictionary<int, int>();
            Dictionary<DeputeModel, int> frondeurs = new Dictionary<DeputeModel, int>();
            foreach (var (d, s) in res)
            {
                if(!frondeurs.ContainsKey(d))
                {
                    frondeurs.Add(d, 0);
                }

                csv.Add(string.Format($"{d.Prenom} {d.Nom},{s.Count}"));
                csv.Add(string.Format($",position personnelle,position LREM,date,titre"));
                foreach(var sc in s)
                {
                    frondeurs[d] += 1;
                    var lremVote = sc.Results.First(g => g.Groupe.Nom.Contains("Marche")).PositionMajoritaire;
                    var vote = sc.GetVote(d);
                    csv.Add(string.Format($",{vote},{lremVote},{sc.Date:MM/dd/yyyy},scrutin n° {sc.Numero} : {sc.Titre.Replace(',', ' ')}"));
                    if(!votesPerDate.ContainsKey(sc.Date))
                    {
                        votesPerDate.Add(sc.Date, 0);
                    }
                    votesPerDate[sc.Date] += 1;

                    if(!votesPerScrutin.ContainsKey(sc.Numero))
                    {
                        votesPerScrutin.Add(sc.Numero, 0);
                    }
                    votesPerScrutin[sc.Numero] += 1;
                }
            }

            File.WriteAllLines("eds.csv", csv, Encoding.UTF8);
            File.WriteAllLines("dates.csv", votesPerDate.Select(kvp => kvp.Key.ToString("MM/dd/yyyy") + "," + kvp.Value));
            File.WriteAllLines("scrutins.csv", votesPerScrutin.Select(kvp => kvp.Key + "," + kvp.Value));
            File.WriteAllLines("frondeurs.csv", frondeurs.Select(kvp => kvp.Key.Prenom + " " + kvp.Key.Nom + "," + kvp.Value));
        }
    }
}

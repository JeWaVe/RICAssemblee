using RICAssemblee.DataImport.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LocalTests
{
    class Program
    {
        class GroupeCount
        {
           public int Accords { get; set; }
            public int Votes { get; set; }
        }

        static void Main(string[] args)
        {
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");
            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");
            //
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/loi/scrutins/Scrutins_XV.json.zip", "scrutins.zip");
            //ZipFile.ExtractToDirectory("scrutins.zip", "scrutins");

            var factory = new ModelFactory();
            string parentDir = Path.Combine(Environment.CurrentDirectory, "acteurs");
            var organes = factory.Organes(parentDir);
            var deputes = factory.Deputes(parentDir);

            var scrutins = factory.Scrutins(Path.Combine(Environment.CurrentDirectory, "scrutins", "json"))
                .OrderByDescending(s => s.TotalExprimes).ToList();

#if false // compatibilite des groupes parlementaires
            var allGps = ObjectStorage<BaseModel>.Singleton().All
                .Where(o => o is GroupeParlementaireModel)
                .Select(o => o as GroupeParlementaireModel)
                .Where(g => g.Legislature == 15 && g.Active);

            var compatibilites = new SortedDictionary<GroupeParlementaireModel, SortedDictionary<GroupeParlementaireModel, GroupeCount>>();
            
            foreach (var gp in allGps)
            {
                compatibilites.Add(gp, new SortedDictionary<GroupeParlementaireModel, GroupeCount>());
                foreach (var gp2 in allGps)
                {
                    compatibilites[gp].Add(gp2, new GroupeCount());
                }
            }
            foreach(var scrutin in scrutins)
            {
                foreach(var result in scrutin.Results)
                {
                    if (!result.Groupe.Active)
                        continue;
                    var pos = result.PositionMajoritaire;
                    foreach(var result2 in scrutin.Results)
                    {
                        if (!result2.Groupe.Active)
                            continue;
                        var pos2 = result2.PositionMajoritaire;
                        if (pos == pos2)
                        {
                            compatibilites[result.Groupe][result2.Groupe].Accords += 1;
                        }

                        compatibilites[result.Groupe][result2.Groupe].Votes += 1;
                    }
                }
            }

            List<string> lines = new List<string>();

            lines.Add(" X ," + String.Join(",", allGps.Select(g => g.Nom.Replace(',', ' '))));
            foreach (var kvp in compatibilites)
            {
                StringBuilder line = new StringBuilder();
                line.Append(kvp.Key.Nom.Replace(',', ' '));
                foreach (var gp in allGps)
                {
                    var count = kvp.Value[gp];
                    var val = count.Accords / (float)count.Votes;
                    line.Append(',').Append(float.IsNaN(val) ? -1 : val);
                }
                lines.Add(line.ToString());
            }
            
            
            File.WriteAllLines("compatibilite.csv", lines, Encoding.UTF8);
#endif


#if false // cohérence politique des groupes parlementaires
            Dictionary<GroupeParlementaireModel, List<float>> resultByGroup = new Dictionary<GroupeParlementaireModel, List<float>>();
            Dictionary<GroupeParlementaireModel, int> desaccordsParGroupe = new Dictionary<GroupeParlementaireModel, int>();

            foreach (var scrutin in scrutins)
            {
                foreach(var result in scrutin.Results)
                {
                    if(!resultByGroup.ContainsKey(result.Groupe))
                    {
                        resultByGroup.Add(result.Groupe, new List<float>());
                    }
                    if (!desaccordsParGroupe.ContainsKey(result.Groupe))
                    {
                        desaccordsParGroupe.Add(result.Groupe, 0);
                    }

                    var data = new int[] { result.Pour.Count, result.Contre.Count };
                    int max = data.Max();
                    int total = data.Sum();
                    float percentage = max / (float)total;
                    if (!float.IsNaN(percentage))
                    {
                        resultByGroup[result.Groupe].Add(percentage);
                        if(percentage < 0.75)
                        {
                            desaccordsParGroupe[result.Groupe] += 1;
                        }
                    }
                }
            }

            var averages = resultByGroup.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Average());
            var lines = new List<string> { "groupe,métrique" };
            lines.AddRange(averages.Select(kvp =>
                string.Format($"{kvp.Key.Nom.Replace(',', ' ')},{kvp.Value}")
            ));
            var lines2 = new List<string> { "groupe,métrique" };
            lines2.AddRange(desaccordsParGroupe.Select(kvp =>
                string.Format($"{kvp.Key.Nom.Replace(',', ' ')},{kvp.Value}")
            ));


            File.WriteAllLines("resultByGroup.csv", lines, Encoding.UTF8);
            File.WriteAllLines("desaccordsParGroupe.csv", lines2, Encoding.UTF8);
            return;
#endif

#if false // permanences parlementaires
            var addresses = deputes.Where(d => d.Adresses.FirstOrDefault(a => a.Type == RICAssemblee.DataImport.RawData.AdresseType.Circonscription) != null)
                .Select(d => (
                        d.Adresses.First(add => add.Type == RICAssemblee.DataImport.RawData.AdresseType.Circonscription) as AdressePostaleModel,
                        string.Format($"{d.Prenom.Replace(',', ' ')} {d.Nom.Replace(',', ' ')}")))
                .Select(item => String.Format($"{item.Item1.NumeroRue?.Replace(',', ' ')} {item.Item1.NomRue?.Replace(',', ' ')} {item.Item1.CodePostal} {item.Item1.Ville}, {item.Item2}"));
            File.WriteAllLines("addresses.csv", addresses, Encoding.UTF8);
            return;
#endif
#if false // EDS analysis
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
#endif
        }
    }
}

using RICAssemblee.DataImport.RawData;
using System;
using System.IO;
using System.Linq;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");

            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");

            var allDeputes = Directory.GetFiles("acteurs/acteur").Select(f =>
            {
                string content = File.ReadAllText(f);
                try
                {
                    var result = RawActeur.FromJson(content).Acteur;

                    return result;
                }
                catch (Exception e)
                {
                    return null;
                }
            }).ToList();
        }
    }
}

using RICAssemblee.DataImport.Models;
using RICAssemblee.DataImport.RawData;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");

            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");
            var deputes = ModelFactory.MakeDeputes("acteurs");


            var adresse = deputes
                .First() // select by name here
                .Adresses
                .First(add => add.Type == AdresseType.Circonscription);

            var tmp = adresse;

            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/loi/scrutins/Scrutins_XV.json.zip", "scrutins.zip");
            //ZipFile.ExtractToDirectory("scrutins.zip", "scrutins");
            foreach (var f in System.IO.Directory.GetFiles("scrutins/json"))
            {
                var scrutin = RawScrutin.FromJson(System.IO.File.ReadAllText(f));
                var ziou = scrutin.Scrutin.VentilationVotes.OrganeScrutin.Groupes.Groupe[0];
            }

        }
    }
}

using RICAssemblee.DataImport.Models;
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
            var deputes = ModelFactory.MakeDeputes("acteurs").OrderByDescending(d => d.Mandats.Count()).ToArray();
            var JLM = deputes.First(d => d.Nom.Contains("enchon"));
            //var tmp = allOrganes.Where(d => d.Parent != null).ToList();

        }
    }
}

using RICAssemblee.DataImport.Models;
using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");

            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");

            var factory = new ModelFactory();
            string parentDir = Path.Combine(Environment.CurrentDirectory, "acteurs");
            var organes = factory.Organes(parentDir);
            var deputes = factory.Deputes(parentDir);

            var allGP = ObjectStorage<BaseModel>.Singleton().All
                .Where(v => v is GroupeParlementaireModel)
                .Select(v => v as GroupeParlementaireModel)
                .Where(gp => gp.Active)
                .ToList();

            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/loi/scrutins/Scrutins_XV.json.zip", "scrutins.zip");
            //ZipFile.ExtractToDirectory("scrutins.zip", "scrutins");

            var scrutins = factory.Scrutins(Path.Combine(Environment.CurrentDirectory, "scrutins", "json")).ToList();
        }
    }
}
